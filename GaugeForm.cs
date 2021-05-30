using GaugeReader.Extensions;
using GaugeReader.Images.Models;
using GaugeReader.Processors;
using GaugeReader.Processors.Models;
using GaugeReader.Profiles.Helpers;
using GaugeReader.Profiles.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GaugeReader
{
    public partial class GaugeForm : Form
    {
        List<Processor> processors;

        public GaugeForm()
        {
            InitializeComponent();

            processors = new List<Processor>()
            {
                new LoadImageProcessor(),
                new SelectProfileProcessor(),

                // Isolate gauge from image
                new LocateGaugeProcessor(),

                // Isolate dial
                new IsolateMarkerProcessor("Thermometer", "Hygrometer"),

                // Find hand
                new HandLineProcessor(),
                new CenterImageProcessor(),
                new HandAngleProcessor(),
                
                // Find markers
                new MarkerConvolutionProcessor(),

                // Find result
                new ResultProcessor()
            };
        }

        private void GaugeForm_Load(object sender, EventArgs e)
        {
            // Setup profile selector
            ProfileHelper.GetProfiles().ForEach(p => ProfileComboBox.Items.Add(p));

            if (!string.IsNullOrEmpty(Constants.DefaultPath))
                ProcessImage(Constants.DefaultPath);
        }

        public void ProcessImage(string filepath)
        {
            var args = new ProcessorArgs()
            {
                ImageFile = new ImageFile(filepath),
                Profile = ProfileComboBox.SelectedItem as IProfile
            };

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var processor in processors)
            {
                var result = processor.Run(args);

                foreach (var message in result.Messages)
                    Log(message.Message, message.Debug);

                if (DebugCheckBox.Checked)
                    foreach (var debugImage in result.DebugImage)
                        AddOutputImage(debugImage);

                if (result.Skipped)
                    continue;

                if (args.Aborted)
                {
                    stopWatch.Stop();
                    Log($"File: {filepath} failed. {processor.Name} aborted after {stopWatch.ElapsedMilliseconds}", false);
                    var abortedImage = args.ImageSet.GetUnfilteredImage();
                    abortedImage.DrawText("Aborted", Color.Red);
                    AddOutputImage(new OutputImage(abortedImage, 200, 200, $"{processor.Name}: Aborted"));

                    return;
                }
            }

            stopWatch.Stop();

            if (args.ExpectedValue.HasValue)
            {
                if (args.Passed.GetValueOrDefault())
                    Log($"File: {filepath} passed using {args.Profile.Name} profile in {stopWatch.ElapsedMilliseconds} ms", false);
                else
                    Log($"File: {filepath} failed using {args.Profile.Name} profile in {stopWatch.ElapsedMilliseconds} ms", false);
            }
            else
            {
                Log($"File: {filepath} finished using {args.Profile.Name} profile in {stopWatch.ElapsedMilliseconds} ms", false);
            }


            AddOutputImage(new OutputImage(args.ResultImage.GetUnfilteredImage(), 200, 200, $"Result: {args.Profile.Reading(args.ActualValue.GetValueOrDefault())}"));
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
                ProcessImage(OpenFileDialog.FileName);
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

            string[] allfiles = Directory.GetFiles("TestSets", "*.jpg", SearchOption.AllDirectories);

            foreach (var path in allfiles)
                ProcessImage(path);
        }

        private void TestFolderButton_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Clear();
                string[] allfiles = Directory.GetFiles(FolderBrowserDialog.SelectedPath, "*.jpg", SearchOption.AllDirectories);
                foreach (var path in allfiles)
                    ProcessImage(path);
            }
        }
    }
}
