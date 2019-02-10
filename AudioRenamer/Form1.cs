using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

namespace AudioRenamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Length of the movie (in seconds)
        /// </summary>
        private int MovieLength = 0;

        /// <summary>
        /// List of the files to work on
        /// </summary>
        private List<FileInfo> Files = new List<FileInfo>();

        /// <summary>
        /// List of the new names of the files
        /// </summary>
        private List<string> NewNames = new List<string>();

        /// <summary>
        /// Match an audio file
        /// </summary>
        private static Regex AudioFile = new Regex(@"^(.+_)?F\d+_(T\d+)_Audio\s+\-\s+(.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Known audio formats, and their nice names
        /// </summary>
        private static Dictionary<string, string> KnownAudioFormats = new Dictionary<string, string>()
        {
            { ".dts", "DTS" },
            { ".dtshd", "DTS-HD" },
            { ".ac3", "AC3" },
            { ".eac3", "E-AC3" },
            { ".mp4", "AAC" },
            { ".m4a", "AAC" },
            { ".thd", "TrueHD" },
            { ".flac", "FLAC" },
        };

        /// <summary>
        /// Select all text in a NumericUpDown control
        /// </summary>
        /// <param name="sender">The control to use</param>
        /// <param name="e">Args, ignored</param>
        private void numericUpDown_SelectAll(object sender, EventArgs e)
        {
            if (sender is NumericUpDown)
            {
                NumericUpDown control = (NumericUpDown)sender;
                control.Select(0, control.Text.Length);
            }
        }

        private void numericUpDownMinutes_ValueChanged(object sender, EventArgs e)
        {
            while (numericUpDownMinutes.Value >= 60)
            {
                numericUpDownHours.Value++;
                numericUpDownMinutes.Value -= 60;
            }
            while (numericUpDownMinutes.Value < 0)
            {
                if (numericUpDownHours.Value > 0)
                {
                    numericUpDownHours.Value--;
                    numericUpDownMinutes.Value += 60;
                }
                else
                {
                    numericUpDownMinutes.Value = 0;
                }
            }

            // Update total
            numericUpDown_ValueChanged(sender, e);
        }

        private void numericUpDownSeconds_ValueChanged(object sender, EventArgs e)
        {
            while (numericUpDownSeconds.Value >= 60)
            {
                numericUpDownMinutes.Value++;
                numericUpDownSeconds.Value -= 60;
            }
            while (numericUpDownSeconds.Value < 0)
            {
                if ((numericUpDownMinutes.Value > 0) || (numericUpDownHours.Value > 0))
                {
                    numericUpDownMinutes.Value--;
                    numericUpDownSeconds.Value += 60;
                }
                else
                {
                    numericUpDownSeconds.Value = 0;
                }
            }

            // Update total
            numericUpDown_ValueChanged(sender, e);
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MovieLength = (int)((((numericUpDownHours.Value * 60) + numericUpDownMinutes.Value) * 60) + numericUpDownSeconds.Value);
            textBoxSeconds.Text = MovieLength.ToString("n0");
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (File.Exists(filePath))
                {
                    FileInfo fi = new FileInfo(filePath);
                    addFile(fi);
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFiles(true);
        }

        /// <summary>
        /// Clear the files
        /// </summary>
        /// <param name="all">Include ignored files?</param>
        private void clearFiles(bool all)
        {
            Files.Clear();
            NewNames.Clear();
            listBoxSource.Items.Clear();
            listBoxNewName.Items.Clear();
            if (all)
            {
                listBoxIgnored.Items.Clear();
                listBoxIgnoredWhy.Items.Clear();
            }
        }

        /// <summary>
        /// Add a file to process
        /// </summary>
        /// <param name="file">The file to process</param>
        private void addFile(FileInfo file)
        {
            // Validate extension
            string niceFormatName;
            string extension = file.Extension.ToLower();
            if (KnownAudioFormats.ContainsKey(extension))
            {
                niceFormatName = KnownAudioFormats[extension];
            }
            else
            {
                // Unknown file type
                listBoxIgnored.Items.Add(file.Name);
                listBoxIgnoredWhy.Items.Add("Unknown file format");
                return;
            }

            // Validate file
            string name = Path.GetFileNameWithoutExtension(file.Name);
            Match audioMatch = AudioFile.Match(name);
            string newName = name;
            if (audioMatch.Success)
            {
                // We can make a fancy new name!
                string trackNo = audioMatch.Groups[2].Value;
                string mainName = audioMatch.Groups[3].Value;

                newName = trackNo + " " + mainName;
            }

            // Add format name
            newName += " " + niceFormatName;

            // Append bitrate
            string bitrate = getBitrate(file, MovieLength);
            newName += " " + bitrate + extension;

            // Store & display results
            Files.Add(file);
            NewNames.Add(newName);
            listBoxSource.Items.Add(file.Name);
            listBoxNewName.Items.Add(newName);
        }

        /// <summary>
        /// Get the bitrate of a file, in bits/sec
        /// </summary>
        /// <param name="file">The file</param>
        /// <param name="length">The length of the file, in seconds</param>
        /// <returns>Formatted bitrate string</returns>
        private string getBitrate(FileInfo file, int length)
        {
            // Handle the stupid
            if (length <= 0)
            {
                return "Unknown Bitrate";
            }

            // Convert bytes to bits (*8)
            float bps = (float)(file.Length * 8) / (float)length;
            float kbps = bps / 1024f;
            float Mbps = kbps / 1024f;
            float Gbps = Mbps / 1024f;

            string value;
            if (Gbps >= 1f)
            {
                if (Gbps >= 100f)
                {
                    // WTF!?!
                    value = Gbps.ToString("f0");
                }
                else
                {
                    // Wow!
                    value = Gbps.ToString("f1");
                }
                return value + "Gbps";
            }
            else if (Mbps >= 1f)
            {
                if (Mbps >= 100f)
                {
                    // Impressive
                    value = Mbps.ToString("f0");
                }
                else
                {
                    // HD / lossless, baby!
                    value = Mbps.ToString("f1");
                }
                return value + "Mbps";
            }
            else if (kbps >= 1f)
            {
                if (kbps >= 100f)
                {
                    // Typical
                    value = kbps.ToString("f0");
                }
                else
                {
                    // Impressive
                    value = kbps.ToString("f1");
                }
                return value + "kbps";
            }
            else
            {
                if (bps >= 100f)
                {
                    // Wow!
                    value = bps.ToString("f0");
                }
                else
                {
                    // WTF!?!
                    value = bps.ToString("f1");
                }
                return value + "bps";
            }
        }

        private void listBoxSource_Scroll(object Sender, BetterListBox.BetterListBoxScrollArgs e)
        {
            listBoxNewName.TopIndex = e.Top;
        }

        private void listBoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxNewName.SelectedIndex = listBoxSource.SelectedIndex;
        }

        private void listBoxIgnored_Scroll(object Sender, BetterListBox.BetterListBoxScrollArgs e)
        {
            listBoxIgnoredWhy.TopIndex = e.Top;
        }

        private void listBoxIgnored_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxIgnoredWhy.SelectedIndex = listBoxIgnored.SelectedIndex;
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            // Actually rename the files
            for (int i = 0; i < Files.Count; i++)
            {
                string oldName = Files[i].FullName;
                string oldPath = Path.GetDirectoryName(oldName);
                string newName = Path.Combine(oldPath, NewNames[i]);
                try
                {
                    File.Move(oldName, newName);
                }
                catch (Exception ex)
                {
                    listBoxIgnored.Items.Add(oldName);
                    listBoxIgnoredWhy.Items.Add("Exception: " + ex.Message);
                }
            }

            // Cleanup!
            clearFiles(false);
        }
    }
}
