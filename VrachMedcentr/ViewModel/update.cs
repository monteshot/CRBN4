using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VrachMedcentr.View;

namespace VrachMedcentr
{
    class update : INotifyPropertyChanged
    {

        string updateString = "http://skusch.16mb.com/MED/Medicine_Setup.msi";
        string verString = "http://skusch.16mb.com/MED/ver.txt";
        string batString = "http://skusch.16mb.com/MED/update.txt";
        string vbsString = "http://skusch.16mb.com/MED/start.txt";

        bool newVerAvailble;
        string remoteVer;
        Version currVer;
        public int progressBarValue = 0;
        public int progressBarValueProp { get; set; }
        public string NameFile { get; set; }
        string executionDirectory = Environment.CurrentDirectory;// Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        WebClient web = new WebClient();

        public update()
        {
            getVersion();

        }
        private RelayCommand _downloadPacket;
        public RelayCommand downloadPacket
        {
            get
            {
                return _downloadPacket ??
                  (_downloadPacket = new RelayCommand(obj =>
                  {

                      //SelectedDocNames.docBool = TimeHour;                   
                      // ListOfDocNames = con.GetDoctrosNames(SelectedSpecf.idspecf.ToString());
                      try
                      {
                          GetInstaller();

                      }
                      catch
                      {

                      }




                  }));
            }
        }

        bool becomeUpdate = false;
        public async void getVersion()
        {

            try
            {
                currVer = Assembly.GetExecutingAssembly().GetName().Version;
                remoteVer = await web.DownloadStringTaskAsync(verString);
                //string[] remoteVerParsed = remoteVer.Split(new char[] { '.' });
                //string[] currVerParsed = currVer.ToString().Split(new char[] { '.' });
                //for (int i = 0; i < remoteVerParsed.Length - 1; i++)
                //{
                //    if (remoteVerParsed[i] != currVerParsed[i])
                //    {
                //        becomeUpdate = true;
                //    }
                //    else
                //    {
                //        becomeUpdate = false;
                //    }
                //}
                if (remoteVer != currVer.ToString())
                {
                    newVerAvailble = true;
                    var result = MessageBox.Show("Завантажити оновлення програмного пакету?", "Доступне оновлення програми", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No) { }
                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Починається завантаження програми\n" +
                            "Для продовження натисніть \"ОК\"" +
                            "\nПрограма встановлення автоматично запуститься", "Відбувається завантаження", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetInstaller();
                    }
                }
                else { newVerAvailble = false; }

            }
            catch { }
        }

        string FileNameCuter(string inString)
        {
            string[] outString = inString.Split(new char[] { '/' });
            return outString[4];
        }

        string fileName = "";
        UpdateView updateView = new UpdateView();
        //static update upd= this;
        public async void GetInstaller()
        {
            try
            {
                updateView.DataContext = this;
                updateView.Show();
           //     updateView.TitleString = remoteVer;
                NameFile = "Завантаження, зачекайте...";
                web.DownloadProgressChanged += Web_DownloadProgressChanged;
                web.DownloadFileCompleted += Web_DownloadFileCompleted;



                NameFile = String.Format("Завантаження: {0}", FileNameCuter(vbsString));
                await web.DownloadFileTaskAsync(new Uri(vbsString), executionDirectory + "\\start.vbs");
                NameFile = String.Format("Завантаження: {0}", FileNameCuter(batString));
                await web.DownloadFileTaskAsync(new Uri(batString), executionDirectory + "\\update.bat");
                NameFile = String.Format("Завантаження: {0}", FileNameCuter(updateString));
                await web.DownloadFileTaskAsync(new Uri(updateString), executionDirectory + "\\Medicine_Setup.msi");

            }
            catch (Exception e)
            {// сдесь вылазит эсепшен по невозможности веб клиенту работать асинхронно, но он, сука, работает!
                // MessageBox.Show(e.Message);
            }
            finally
            {

                Process.Start(executionDirectory + "\\start.vbs");
                Environment.Exit(0);
            }

        }

        private void Web_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {

        }

        private void Web_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {


        }

        private void Web_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBarValueProp = int.Parse(Math.Truncate(percentage).ToString());
            progressBarValue = progressBarValueProp;

        }



        //public Task GetVersionTask()
        //{

        //    return Task.Run(() =>
        //    {

        //        try
        //        {

        //            currVer = Assembly.GetExecutingAssembly().GetName().Version;

        //            //   remoteVer = web.DownloadString("https://drive.google.com/uc?export=download&id=0B1PRhPmv7AwwelZaZEs0ZUljcms"); //скоро прямые сслки не будут работать
        //            if (remoteVer != currVer.ToString())
        //            {
        //                newVerAvailble = true;

        //            }

        //            //if (newVerAvailble == true)
        //            //{
        //            //    HttpWebRequest dwnFile;
        //            //    //пока ничо не делает
        //            //}
        //        }
        //        catch (Exception e) { MessageBox.Show(e.Message); }


        //    });
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
