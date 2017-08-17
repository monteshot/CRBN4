using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace VrachMedcentr
{
    class EditDayViewModel : INotifyPropertyChanged
    {

        public EditDayViewModel(DocNames Doc)
        {
            WorkDays = con.GetListOfWorkingDays(Convert.ToInt32(Doc.docID));
            docSelected = Doc;
            selectedDays = DateTime.Now;
        }
        private DateTime _selectedDays;
        public DateTime selectedDays
        {
            get
            {
                return _selectedDays;
            }
            set
            {
                try
                {
                    _selectedDays = value;
                    docTimes = new ObservableCollection<Times>();
                    if (WorkDays.Contains(value) == true)
                    {
                        List<Times> temp = con.getDocTimes(docSelected.docID, value);
                        foreach (var a in temp)
                        {
                            docTimes.Add(new Times { Time = a.Time, Label = a.Label, Status = a.Status });
                        }
                        //docTimes = docTimes.OrderBy(p => p.Time) as ObservableCollection<Times>;


                    }
                    else
                    {
                        docTimes = new ObservableCollection<Times>();
                        docTimes.Add(new Times { Label = "Не робочій день", Status = "Red" });
                    }




                }
                catch (Exception e)
                {
                   // MessageBox.Show(e.Message.ToString());
                   
                }

            }
        }
        public DocNames docSelected { get; set; }
        public ObservableCollection<Times> docTimes { get; set; }
        public ObservableCollection<DateTime> WorkDays { get; set; }
        conBD con = new conBD("shostka.mysql.ukraine.com.ua", "shostka_crl", "shostka_crl", "Cpu25Pro");
        SynhronyzeClass sync = new SynhronyzeClass();
        List<DateTime> selectedDaysCal = new List<DateTime>();



        public ObservableCollection<DocNames> docBool { get; set; }

        public Times SelectedTime { get; set; }




        private RelayCommand _addTimes;
        public RelayCommand addTimes
        {
            get
            {

                return _addTimes ??
                  (_addTimes = new RelayCommand(obj =>
                  {

                      docTimes.Add(new Times { Time = "12:00", Label = "12:00" });
                      // NotifyPropertyChanged("docTimes");



                  }));
            }

        }

        /// <summary>
        /// команда удаления записи в качесте obj передаеться пораметр команды с общим Binding
        /// </summary>
        private RelayCommand _remTimes;

        public RelayCommand remTimes
        {
            get
            {

                return _remTimes ??
                  (_remTimes = new RelayCommand(obj =>
                  {

                      try
                      {

                          docTimes.Remove(obj as Times);
                          foreach (var a in tempSelected as SelectedDatesCollection)
                          {
                              Times temp = obj as Times;
                              string[] parTime = temp.Time.Split(new char[] { ':' });
                              con.RemTimeInWorkDay(docSelected.docID, a, parTime[0], parTime[1]);

                          }


                      }
                      catch (Exception) { }



                  }));
            }

        }




        private RelayCommand _setSelectedDays;

        public RelayCommand setSelectedDays
        {
            get
            {

                return _setSelectedDays ??
                  (_setSelectedDays = new RelayCommand(obj =>
                  {
                      tempSelected = obj as SelectedDatesCollection;

                  }));
            }

        }
        SelectedDatesCollection tempSelected;

        private RelayCommand _checkDays;

        public RelayCommand checkDays
        {
            get
            {

                return _checkDays ??
                  (_checkDays = new RelayCommand(obj =>
                  {


                      string datestring = "";
                      List<DateTime> DateWithoutWorkingDays = new List<DateTime>();
                      try
                      {
                          foreach (var a in tempSelected as SelectedDatesCollection)
                          {
                              if (WorkDays.Contains(a) == true)
                              {
                                  datestring = datestring + a.ToShortDateString()+", ";
                              }
                              else
                              {
                                  DateWithoutWorkingDays.Add(a);
                              }
                          }
                          datestring = datestring.Remove(datestring.Length - 2);
                          if (datestring != "")
                          {
                              var result = MessageBox.Show("На обрану  дату(и): " + datestring + " вже існує розклад.\nПерезаписати розклад на ці дні?", "Повідомлення", MessageBoxButton.YesNo, MessageBoxImage.Question);
                              if (result == MessageBoxResult.Yes)
                              {
                                  foreach (var a in tempSelected as SelectedDatesCollection)
                                  {
                                      con.remWorkDays(docSelected.docID, a);
                                      foreach (var t in docTimes)
                                      {
                                          string[] parTime = t.Time.Split(new char[] { ':' });

                                          con.addWorkDays(docSelected.docID, "0", false, true, a, parTime[0], parTime[1], "0", "0");
                                      }
                                  }
                                  datestring = "";
                                  DateWithoutWorkingDays = new List<DateTime>();
                              }
                              if (result == MessageBoxResult.No)
                              {
                                  foreach (var a in DateWithoutWorkingDays )
                                  {

                                      foreach (var t in docTimes)
                                      {
                                          string[] parTime = t.Time.Split(new char[] { ':' });

                                          con.addWorkDays(docSelected.docID, "0", false, true, a, parTime[0], parTime[1], "0", "0");
                                      }

                                  }
                                  datestring = "";
                                  DateWithoutWorkingDays = new List<DateTime>();
                              }
                          }
                          else
                          {
                              foreach (var a in tempSelected as SelectedDatesCollection)
                              {

                                  foreach (var t in docTimes)
                                  {
                                      string[] parTime = t.Time.Split(new char[] { ':' });

                                      con.addWorkDays(docSelected.docID, "0", false, true, a, parTime[0], parTime[1], "0", "0");
                                  }

                              }
                          }

                          
                          WorkDays = con.GetListOfWorkingDays(Convert.ToInt32(docSelected.docID));
                      }
                      catch (Exception e)
                      {
                      //    MessageBox.Show(e.ToString());
                      }

                      //editDays edDays = new editDays();
                      // sync.SynhronyzeTable("enx4w_ttfsp", 2);

                      // edDays.Close();

                  }));
            }

        }

        private RelayCommand _uncheckDays;

        public RelayCommand uncheckDays
        {
            get
            {

                return _uncheckDays ??
                  (_uncheckDays = new RelayCommand(obj =>
                  {



                      try
                      {
                          foreach (var a in tempSelected as SelectedDatesCollection)
                          {


                              con.remWorkDays(docSelected.docID, a);

                          }
                          WorkDays = con.GetListOfWorkingDays(Convert.ToInt32(docSelected.docID));
                          // sync.SynhronyzeTable("enx4w_ttfsp", 2);
                      }
                      catch (Exception) { }


                  }));
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
