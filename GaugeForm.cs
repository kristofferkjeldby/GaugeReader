using GaugeReader.Extensions;
using GaugeReader.Models.Angles;
using GaugeReader.Models.Gauges;
using GaugeReader.Models.Processors;
using GaugeReader.Processors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GaugeReader
{
    public partial class GaugeForm : Form
    {
        List<Processor> processors;
        List<GaugeProfile> profiles;

        public GaugeForm()
        {
            InitializeComponent();

            processors = new List<Processor>()
            {
                // Isolate gauge from image
                new ScaleProcessor(),
                new EdgeProcessor(),
                new InvertProcessor(),
                new DialProcessor(new RadiusZone(0.2, 1)),

                // Isolate dial from thermometer
                new DialProcessor(new RadiusZone(0.6, 0.8), Constants.Themometer),
                new EdgeProcessor(Constants.Themometer),

                // Find hand
                new HandLineProcessor(),
                new CenterImageProcessor(Constants.Simple),
                new HandAngleProcessor(),
                
                // Find markers
                new MarkerSymmetryProcessor(Constants.Simple),
                new MarkerConvolutionProcessor(Constants.Themometer),

                // Find result
                new ResultProcessor()
            };

            profiles = new List<GaugeProfile>()
            {
                Constants.Simple,
                Constants.Themometer
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProfileComboBox.DisplayMember = "Name";

            foreach(var profile in profiles)
            {
                ProfileComboBox.Items.Add(profile);
            }

            ProfileComboBox.SelectedIndex = 0;

            ProcessImage("TrainingSet/Thermometer/thermometer_10_ex_57.jpg", ProfileComboBox.SelectedItem as GaugeProfile);
        }

        public void ProcessImage(string path, GaugeProfile profile)
        {
            var filename = Path.GetFileNameWithoutExtension(path);

            var args = new ProcessorArgs()
            {
                Profile = profile,
                OriginalImage = Image.FromFile(path) as Bitmap,
                Path = path
            };

            // Override profile
            if (filename.Contains("_"))
            {
                var fileProfile = filename.Split('_').First();
                if (profiles.Any(p => p.Name.Equals(fileProfile, StringComparison.InvariantCultureIgnoreCase)))
                {
                    args.Profile = profiles.First(p => p.Name.Equals(fileProfile, StringComparison.InvariantCultureIgnoreCase));
                }
            }

            if (filename.Contains("_ex_"))
            {
                args.ExpectedValue = Convert.ToInt32(filename.Split('_').Last());
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var processor in processors)
            {
                var result = processor.Run(args);

                foreach (var message in result.Messages)
                {
                    Log(message.Message, message.Debug);
                }

                if (DebugCheckBox.Checked)
                { 
                    foreach (var debugImage in result.DebugImage)
                    {
                        AddOutputImage(debugImage);
                    }
                }

                if (result.Skipped)
                {
                    continue;
                }

                if (args.Aborted)
                {
                    stopWatch.Stop();
                    Log($"File: {path} failed. {processor.Name} aborted after {stopWatch.ElapsedMilliseconds}", false);
                    var abortedImage = args.ScaledImage;
                    abortedImage.DrawText("Aborted", Color.Red);
                    AddOutputImage(new OutputImage(abortedImage, "Aborted", 200, 200));

                    return;
                }
            }

            stopWatch.Stop();

            if (args.ExpectedValue.HasValue)
            {
                if (args.Passed.GetValueOrDefault())
                    Log($"File: {path} passed using {args.Profile.Name} profile in {stopWatch.ElapsedMilliseconds} ms", false);
                else
                    Log($"File: {path} failed using {args.Profile.Name} profile in {stopWatch.ElapsedMilliseconds} ms", false);
            }
            else
            {
                Log($"File: {path} finished using {args.Profile.Name} profile in {stopWatch.ElapsedMilliseconds} ms", false);
            }


            AddOutputImage(new OutputImage(args.ResultImage, args.ActualValue.GetValueOrDefault().ToString(), 200, 200));
        }

        private void AddOutputImage(OutputImage outputImage)
        {
            var panel = new Panel();
            panel.Width = outputImage.Width;
            panel.Height = outputImage.Height + 20;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = outputImage.Width;
            pictureBox.Height = outputImage.Height;
            pictureBox.Image = outputImage.Image;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Top = 0;
            pictureBox.Left = 0;

            panel.Controls.Add(pictureBox);

            var label = new Label();
            label.Text = outputImage.Caption;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Top = outputImage.Height;
            label.Left = 0;
            label.Height = 20;
            label.Width = outputImage.Width;

            panel.Controls.Add(label);

            OutputImageFlowLayoutPanel.Controls.Add(panel);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                Clear();
                ProcessImage(OpenFileDialog.FileName, ProfileComboBox.SelectedItem as GaugeProfile);
            }
        }

        private void Clear()
        {
            LogTextBox.Text = string.Empty;
            OutputImageFlowLayoutPanel.Controls.Clear();
        }

        private void Log(string message, bool debug)
        {
            if (debug && !DebugCheckBox.Checked)
                return;

            if (!string.IsNullOrWhiteSpace(LogTextBox.Text))
                LogTextBox.AppendText(Environment.NewLine);
            LogTextBox.AppendText(message);

        }

        private void TestAllButton_Click(object sender, EventArgs e)
        {
            Clear();

            string[] allfiles = Directory.GetFiles("TrainingSet", "*.jpg", SearchOption.AllDirectories);

            foreach (var path in allfiles)
                ProcessImage(path, Constants.Simple);
        }

        private void TestFolderButton_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Clear();
                string[] allfiles = Directory.GetFiles(FolderBrowserDialog.SelectedPath, "*.jpg", SearchOption.AllDirectories);
                foreach (var path in allfiles)
                    ProcessImage(path, Constants.Simple);
            }
        }
    }
}
