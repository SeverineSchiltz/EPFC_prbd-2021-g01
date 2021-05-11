using prbd_2021_g01.Model;
using PRBD_Framework;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_2021_g01.ViewModel
{
    public class TeacherCourseCategoriesViewModel : ViewModelCommon
    {
        private ObservableCollection<Category> categories = new ObservableCollection<Category>();
        public ObservableCollection<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }


        private IList selectedItems = new ArrayList();
        public IList SelectedItems
        {
            get => selectedItems;
            set => SetProperty(ref selectedItems, value);
        }
        private Course course;
        public Course Course
        {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        public ICommand SaveCategories { get; set; }
        public ICommand DeleteCategory { get; set; }


        public ICollectionView CategoriesView => Categories.GetCollectionView(nameof(Category.Title), ListSortDirection.Descending);

        public TeacherCourseCategoriesViewModel()
        {
            SaveCategories = new RelayCommand(AddNewCategoryAction);

        }

        private void LoadCategories()
        {

            // ne fait rien si pas de membre courant
            if (CurrentUser == null) return;
            // récupère les id's des messages sélectionnés
            var ids = SelectedItems.Cast<Category>().Select(c => c.Id).ToList();
            // Relit les messages en BD et recharge la liste avec ceux-ci. On évite ainsi de recréer une nouvelle collection
            // observable, ce qui nous permet de conserver la custom view et par conséquent l'ordre de tri courant
            Categories.RefreshFromModel(Category.GetCategories(CurrentUser, Course));

            // efface la sélection courante
            SelectedItems.Clear();
            // on re-sélectionne "manuellement" les messages sur base des id's qu'on avait sauvés plus haut
            foreach (var ca in Categories.Where(c => ids.Contains(c.Id)))
                SelectedItems.Add(ca);

            //foreach (var ca in Categories)
            //    SelectedItems.Add(ca);

        }


        private void AddNewCategoryAction()
        {

            List<Category> test = CategoriesView.SourceCollection.Cast<Category>().ToList();
            Category.updateOrAddCategoriesInCourse(test, Course);
        }


        protected override void OnRefreshData()
        {
            LoadCategories();

        }
    }
}
