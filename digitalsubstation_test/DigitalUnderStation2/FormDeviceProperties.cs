using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPAddressControlLib;
using System.Net;

namespace DigitalUnderStation2
{
    public partial class FormDeviceProperties : Form
    {
        IPAddressControl ipaddress = new IPAddressControl();
        IPAddressControl ipmask = new IPAddressControl();
        public class propDev
        {
            public string NameGCB = "";
            public string GOOSEID = "";
            public string MACadress = "";
            public string APPID = "";
            public string VLANID = "";
            public string MinTime = "";
            public string MaxTime = "";
            public string IPAddress = "";
            public string IPMask = "";
            public bool Out1Out1 = false;
            public bool Out1Out2 = false;
            public bool Out1Out3 = false;
            public bool Out2Out1 = false;
            public bool Out2Out2 = false;
            public bool Out2Out3 = false;
            public bool Out3Out1 = false;
            public bool Out3Out2 = false;
            public bool Out3Out3 = false;
            public propDev() { }
        }
        public propDev Prop = new propDev();
        
        public string NameDevice
        {
            get { return labelDeviceName.Text; }
            set { labelDeviceName.Text = value; }
        }

        public void SetProperties(propDev vProp)
        {
            textBoxProp1.Text = vProp.NameGCB;
            textBoxProp2.Text = vProp.GOOSEID;
            textBoxProp3.Text = vProp.MACadress;
            textBoxProp4.Text = vProp.APPID;
            textBoxProp5.Text = vProp.VLANID;
            textBoxProp6.Text = vProp.MinTime;
            textBoxProp7.Text = vProp.MaxTime;
            ipaddress.IPAddress = System.Net.IPAddress.Parse(vProp.IPAddress);
            ipmask.IPAddress = System.Net.IPAddress.Parse(vProp.IPMask);
            checkBoxO1O1.Checked = vProp.Out1Out1;
            checkBoxO1O2.Checked = vProp.Out1Out2;
            checkBoxO1O3.Checked = vProp.Out1Out3;
            checkBoxO2O1.Checked = vProp.Out2Out1;
            checkBoxO2O2.Checked = vProp.Out2Out2;
            checkBoxO2O3.Checked = vProp.Out2Out3;
            checkBoxO3O1.Checked = vProp.Out3Out1;
            checkBoxO3O2.Checked = vProp.Out3Out2;
            checkBoxO3O3.Checked = vProp.Out3Out3;
        }

        public FormDeviceProperties()
        {
            InitializeComponent();
        }

       private void buttonOk_Click(object sender, EventArgs e)
        {
            Prop.NameGCB = textBoxProp1.Text;
            Prop.GOOSEID = textBoxProp2.Text;
            Prop.MACadress = textBoxProp3.Text;
            Prop.APPID = textBoxProp4.Text;
            Prop.VLANID = textBoxProp5.Text;
            Prop.MinTime = textBoxProp6.Text;
            Prop.MaxTime = textBoxProp7.Text;
            Prop.IPAddress = ipaddress.IPAddress.ToString();
            Prop.IPMask = ipmask.IPAddress.ToString();
            Prop.Out1Out1 = checkBoxO1O1.Checked;
            Prop.Out1Out2 = checkBoxO1O2.Checked;
            Prop.Out1Out3 = checkBoxO1O3.Checked;
            Prop.Out2Out1 = checkBoxO2O1.Checked;
            Prop.Out2Out2 = checkBoxO2O2.Checked;
            Prop.Out2Out3 = checkBoxO2O3.Checked;
            Prop.Out3Out1 = checkBoxO3O1.Checked;
            Prop.Out3Out2 = checkBoxO3O2.Checked;
            Prop.Out3Out3 = checkBoxO3O3.Checked;
        }
        private void FormDeviceProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void FormDeviceProperties_Load(object sender, EventArgs e)
        {
            ipaddress.Parent = groupBox2;
            ipaddress.Top = 30;
            ipaddress.Left = 153;

            ipmask.Parent = groupBox2;
            ipmask.Top = 62;
            ipmask.Left = 153;
        }
    }
}
