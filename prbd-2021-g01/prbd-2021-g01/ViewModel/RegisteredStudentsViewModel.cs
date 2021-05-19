using PRBD_Framework;
using prbd_2021_g01.Model;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_2021_g01.ViewModel {
    class RegisteredStudentsViewModel : ViewModelCommon {

        // Récupère une "custom view" sur la liste des registrations. Sur cette vue, on peut définir l'ordre de tri initial
        // (ici on trie sur la date du message dans l'ordre décroissant). Lorsque l'utilisateur change l'ordre de tri
        // en cliquant sur les en-têtes des colonnes, c'est l'ordre de tri de la vue qui est modifié.
        public ICollectionView RegistrationsView => Registrations.GetCollectionView(nameof(Student.Firstname), ListSortDirection.Descending);

        private ObservableCollection<Registration> registrations = new ObservableCollection<Registration>();
        public ObservableCollection<Registration> Registrations {
            get => registrations;
            set => SetProperty(ref registrations, value);
        }

        protected override void OnRefreshData() {
            //if (Member == null) return;
            //Member = Member.GetByPseudo(Member.Pseudo);
            //LoadMessages();
            //RaisePropertyChanged();
        }

    }
}
