using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Windows.Forms;

namespace DigitalUnderStation2
{


    public partial class MainForm1 : Form
    {
        FormLogin fl = new FormLogin();
        FormSelectDevice fsd = new FormSelectDevice();
        FormHubDevices fhd = new FormHubDevices();


        #region Данные к тесту. В будущем отдельный модуль
        public class Qwestion
        {
            public string TextQwestion = "";
            public string TextAnswer1 = "";
            public string TextAnswer2 = "";
            public string TextAnswer3 = "";
            public string TextAnswer4 = "";

            public int Answer = 0;
            public Qwestion()
            {

            }
            public Qwestion(string vTextQwestion, string vTextAnsfer1, string vTextAnsfer2,
                string vTextAnsfer3, string vTextAnsfer4, int vAnsfer)
            {
                TextQwestion = vTextQwestion;
                TextAnswer1 = vTextAnsfer1;
                TextAnswer2 = vTextAnsfer2;
                TextAnswer3 = vTextAnsfer3;
                TextAnswer4 = vTextAnsfer4;

                Answer = vAnsfer;
            }
        }

        public class Test
        {
            public string filename = "http://sch60.aksinet.net:4000/tasks/test1.json";
            public int id = 1;
            public string name = "test1";
            public string title = "Настройка IED на прием-передачу GOOSE-сообщений";
            public Test()
            {

            }
        }

        public class ListTest
        {
            public List<Test> data = new List<Test>();
        }

        public class ItogTest  {
            public string Name = "";
            public string Group = "";
            public string Test = "";
            public List<Qwestion> data = new List<Qwestion>();
        }

        ListTest lt = new ListTest();
        ItogTest Result = new ItogTest();

        //public List<Qwestion> ItogTest = new List<Qwestion>();

        public List<FormDeviceProperties.propDev> ListDeviceJSON = new List<FormDeviceProperties.propDev>();

        /*
         Вот так заполняем список тестов http://sch60.aksinet.net:4000/tests
         потом надо будет залить XML файлы с тестами
         Результаты тестов будут примерно вот так. Пока JSON с результатом я не паршу, но щас займусь
         результаты по HTTP в виде JSONа присылаются
         curl -H "Content-Type: application/json" -X POST http://sch60.aksinet.net:4000/api/results -d '{"username":"Маша", "group":"Группа 1", "test": "2", "answers": {"1":1, "2": 10} }'
         а, ну и приложуха может запросить список тестов http://sch60.aksinet.net:4000/api/tests
         чтобы предложить клиенту их
         */
        #endregion

        public MainForm1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            panelTestObor.AllowDrop = true;
            panelTestObor.Dock = DockStyle.Fill;
            panelBeginShow.Dock = DockStyle.Fill;
            panelSelectTest.Dock = DockStyle.Fill;
            panelItogTest.Dock = DockStyle.Fill;
            panelListGroupDevice.Dock = DockStyle.Fill;
            //ItogTest.Add(new Qwestion("Как расшифровывается аббревиатура IED?",
            //    "Информационно электронное реле",
            //    "Интеллектуальное устройство учета",
            //    "Интеллектуальное электронное устройство",
            //    "Международный энергетический департамент",
            //    3));
            //ItogTest.Add(new Qwestion("Какие сетевые настройки IED влияют на передачу GOOSE-сообщений?",
            //    "MAC –адрес и IP – адрес",
            //    "IP - адрес и VLAN",
            //    "MAC - адрес и APPID",
            //    "Все вместе",
            //    4));
            //ItogTest.Add(new Qwestion("К какому механизму передачи данных относятся GOOSE-сообщения?",
            //    "Клиент-сервер",
            //    "Master-slave",
            //    "Издатель-подписчик",
            //    "Точка-точка",
            //    4));

            //    ItogTest.Add(new Qwestion("Какие первоначальные четыре октета MAC-адреса зарезервировано за ТК57 МЭК?",
            //    "01:0D:BB: 01",
            //    "00:0С: ВB: 01",
            //    "01:0С: CD: 04",
            //    "01:0C: CD: 01",
            //    2));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
            //ItogTest.Add(new Qwestion("",
            //    "",
            //    "",
            //    "",
            //    "",
            //    3));
        }

        private void ButtonRegistration_Click(object sender, EventArgs e)
        {
            if (fl.ShowDialog() == DialogResult.OK)
            {
                //Подключились
                panelSelectTest.Visible = true;
                panelBeginShow.Visible = false;
                //Подгрузка задания




                LoadListTest();
                //panelTest.Visible = true;
                //

            }
            else
            {
                //ждемс и перелистываем названия
            }
        }

        public async void LoadListTest()
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(@"http://sch60.aksinet.net:4000/api/tests");

                lt = JsonConvert.DeserializeObject<ListTest>(json);

                listViewTests.Items.Clear();
                //string s = "";
                foreach (var l in lt.data)
                {
                    ListViewItem lvi = listViewTests.Items.Add("Новый");
                    lvi.SubItems.Add(l.title);
                    lvi.SubItems.Add("15");
                    lvi.SubItems.Add("0 %");
                }
                //Clipboard.SetText(s);

            }
        }

        private void ButtonStartTest_Click(object sender, EventArgs e)
        {
            panelSelectTest.Visible = false;
            panelTestObor.Visible = true;
            timerMsg.Start();
            textBoxHelper.Text = "Добавьте необходимое оборудование и произведите его настройку";
        }

        private void ButtonBackToMainScreen_Click(object sender, EventArgs e)
        {
            panelSelectTest.Visible = false;
            panelBeginShow.Visible = true;
        }

        private void ListViewTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTests.SelectedItems.Count > 0)
            {
                if (listViewTests.SelectedItems[0].Index == 0)
                { buttonStartTest.Enabled = true; }
                else
                { buttonStartTest.Enabled = false; }
            }

        }

        private void ButtonXML_Click(object sender, EventArgs e)
        {
            //using()
            //{
            //    //ucDevice1.fdp.Prop
            //}
            string jsonString;

            ListDeviceJSON.Clear();
            foreach (Control ldc in panelDevicesss.Controls)
            {
                if (ldc.GetType() == typeof(ucDevice))
                {
                    ListDeviceJSON.Add(((ucDevice)ldc).fdp.Prop);

                }
            }
            jsonString = JsonConvert.SerializeObject(ListDeviceJSON);
            //jsonString = JsonConvert.SerializeObject(ItogTest);
            Clipboard.SetText(jsonString);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

         private void buttonCheckDevices_Click(object sender, EventArgs e)
        {
            //Оправляем список оборудования на сервер
            using (var httpClient = new HttpClient())
            {
                string jsonString;

                ListDeviceJSON.Clear();
                foreach (Control ldc in panelDevicesss.Controls)
                {
                    if (ldc.GetType() == typeof(ucDevice))
                    {   ListDeviceJSON.Add(((ucDevice)ldc).fdp.Prop); }
                }
                jsonString = JsonConvert.SerializeObject(ListDeviceJSON);
                var contents = new StringContent(jsonString, Encoding.UTF8, "application/json");
                httpClient.PostAsync(@"http://sch60.aksinet.net:4000/api/tests", contents);
            }

            //читаем список пользователей - там результат проверки


        }

        private void TimerMsg_Tick(object sender, EventArgs e)
        {
            //Периодически проверяем все девайсы и смотрим на отправку сообщения
            foreach (Control ldc in panelDevicesss.Controls)
            {
                bool t = false;
                if (ldc.GetType() == typeof(ucDevice))
                {
                    if (((ucDevice)ldc).SendGOOSE)
                    { t = true;
                        ((ucDevice)ldc).SendGOOSE = false;
                    }
                }

                if (t)
                {
                    //для отправки сообщения
                    //читаем список пользователей - там результат проверки
                    
                    if (t)//Прошли -> итоговый тест
                    { buttonItogTest.Visible = true; }
                }
            }
        }

        private void buttonItogTest_Click(object sender, EventArgs e)
        {
            panelTestObor.Visible = false;
            timerMsg.Stop();
            //Загрузка итогового теста
            LoadItogTest();


            panelItogTest.Visible = true;
        }

        public async void LoadItogTest()
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(@"http://sch60.aksinet.net:4000/api/tests");

                Result = JsonConvert.DeserializeObject<ItogTest>(json);

                foreach (var l in Result.data)
                {
                    //ListViewItem lvi = listViewTests.Items.Add("Новый");
                    //lvi.SubItems.Add(l.title);
                    //lvi.SubItems.Add("15");
                    //lvi.SubItems.Add("0 %");
                }
            }
        }

        private void buttonITCheck_Click(object sender, EventArgs e)
        {
            //Проверка теста
            foreach (Control ldc in panellistQwestion.Controls)
            {
                bool t = false;
                if (ldc.GetType() == typeof(ucQwestion))
                {
                    //((ucQwestion)ldc).resultss
                }

                if (t)
                {
                    //для отправки сообщения
                    //читаем список пользователей - там результат проверки

                    if (t)//Прошли -> итоговый тест
                    { buttonItogTest.Visible = true; }
                }
            }

        }

        private void buttonR3A_Click(object sender, EventArgs e)
        {
            fsd.lMainForm = this;
            fsd.ShowDialog(this);
        }

        public void AddDev01()
        {
            ucDevice ucd = new ucDevice();
            ucd.Parent = panelDevicesss;
            ucd.picture = imageListDevice.Images[0];
            ucd.NameDev = "БМРЗ-50";
            ucd.Top = 100;
            ucd.Left = 150;
            ControlExtension.Draggable(ucd, true);
        }

        public void AddDev02()
        {
            ucDevice ucd = new ucDevice();
            ucd.Parent = panelDevicesss;
            ucd.picture = imageListDevice.Images[1];
            ucd.NameDev = "БМРЗ-100";
            ucd.Top = 100;
            ucd.Left = 150;
            ControlExtension.Draggable(ucd, true);
        }

        public void AddDev03()
        {
            ucDevice ucd = new ucDevice();
            ucd.Parent = panelDevicesss;
            ucd.picture = imageListDevice.Images[2];
            ucd.NameDev = "БМРЗ-150";
            ucd.Top = 100;
            ucd.Left = 150;
            ControlExtension.Draggable(ucd, true);
        }
        public void AddDevHUB()
        {
            PictureBox pb = new PictureBox();
            pb.Parent = panelDevicesss;
            pb.Image = pictureBox4.Image;
            pb.Width = 464;
            pb.Height = 167;
            pb.Top = 150;
            pb.Left = 80;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            ControlExtension.Draggable(pb, true);
        }

        private void buttonPromHub_Click(object sender, EventArgs e)
        {
            fhd.lMainForm = this;
            fhd.ShowDialog(this);
        }
    }
}
