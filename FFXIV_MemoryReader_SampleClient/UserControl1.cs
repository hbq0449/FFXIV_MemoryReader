﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using TamanegiMage.FFXIV_MemoryReader.Base;
using TamanegiMage.FFXIV_MemoryReader.Model;

namespace TamanegiMage.FFXIV_MemoryReader_SampleClient
{
    public partial class UserControl1 : UserControl, IActPluginV1
    {
        Label LabelStatus;

        public UserControl1()
        {
            InitializeComponent();
        }

        public void DeInitPlugin()
        {
            dataGridView1.Rows.Clear();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            this.LabelStatus = pluginStatusText;
            pluginScreenSpace.Controls.Add(this);
            this.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();

            IActPluginV1 plugin = null;
            foreach (var p in ActGlobals.oFormActMain.ActPlugins)
            {
                if (p.pluginFile.Name == "FFXIV_MemoryReader.dll")
                {
                    plugin = p.pluginObj;
                    break;
                }
            }


            if (plugin != null)
            {
                var memoryPlugin = plugin as MemoryPlugin;
                List<Combatant> combatants = memoryPlugin.GetCombatants();
                if (combatants != null)
                {
                    foreach (var combatant in combatants)
                    {
                        this.dataGridView1.Rows.Add(new string[] { combatant.ID.ToString(), combatant.Name, combatant.EffectiveDistance.ToString(), combatant.Heading.ToString() });
                    }
                }
            }
            plugin = null;


        }
    }
}