﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public RichTextBox getRichTextBox()
        {
            return richTextBoxForm3;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

		private void richTextBoxForm3_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
