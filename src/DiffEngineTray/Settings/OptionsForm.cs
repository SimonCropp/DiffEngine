﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class OptionsForm :
    Form
{
    Func<Settings, Task<IReadOnlyList<string>>> trySave = null!;

    public OptionsForm()
    {
        InitializeComponent();
        Icon = Images.Active;
    }

    public OptionsForm(Settings settings, Func<Settings, Task<IReadOnlyList<string>>> trySave):
        this()
    {
        this.trySave = trySave;
        var key = settings.AcceptAllHotKey;
        if (key != null)
        {
            hotKey.KeyEnabled = true;
            hotKey.IsShift =key.Shift;
            hotKey.IsAlt =key.Alt;
            hotKey.IsControl = key.Control;
            hotKey.Key = key.Key;
        }

        startupCheckBox.Checked = settings.RunAtStartup;
    }

    static IEnumerable<string> GetAlphabet()
    {
        for (var c = 'A'; c <= 'Z'; c++)
        {
            yield return c.ToString();
        }
    }

    async void save_Click(object sender, EventArgs e)
    {
        var newSettings = new Settings
        {
            RunAtStartup = startupCheckBox.Checked
        };
        if (hotKey.KeyEnabled)
        {
            newSettings.AcceptAllHotKey = new HotKey
            {
                Key = hotKey.Key!,
                Shift = hotKey.IsShift,
                Alt = hotKey.IsAlt,
                Control = hotKey.IsControl
            };
        }

        var errors = (await trySave(newSettings)).ToList();
        if (!errors.Any())
        {
            DialogResult = DialogResult.OK;
            return;
        }

        var builder = new StringBuilder();
        foreach (var error in errors)
        {
            builder.AppendLine($" * {error}");
        }

        MessageBox.Show(builder.ToString(), "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}