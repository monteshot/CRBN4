using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VrachMedcentr.View;

namespace VrachMedcentr
{
    class ConfirmUserViewModel
    {
        conBD con = new conBD();
        public ObservableCollection<Users> allUsers { get; set; }

        public ConfirmUserViewModel()
        {
            allUsers = con.GetUnConfirmedUsers();
        }
        public void confUserRealization(object obj)
        {
            var selUser = obj as Users;
            con.ConfirmUser(selUser.userId);
            allUsers = con.GetUnConfirmedUsers();
           

        }
        private RelayCommand _confUserCommand;
        public RelayCommand confUserCommand
        {
            get
            {
                return _confUserCommand ??
                       (_confUserCommand = new RelayCommand(obj =>
                       {
                           try
                           {
                               confUserRealization(obj);

                           }
                           catch
                           {

                           }


                       }));
            }
        }
    }
}
