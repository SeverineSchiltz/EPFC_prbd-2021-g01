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
        public ICommand Cancel { get; set; }
        public ICommand DeleteCategories { get; set; }


        public ICollectionView CategoriesView => Categories.GetCollectionView(nameof(Category.Title), ListSortDirection.Descending);

        public TeacherCourseCategoriesViewModel()
        {

            //Course est à null...
            //Categories = new ObservableCollection<Category>(Category.GetCategories(CurrentUser, Course));
            //LoadCategories();

            SaveCategories = new RelayCommand(AddNewCategoryAction);

            Cancel = new RelayCommand(LoadCategories);

            DeleteCategories = new RelayCommand<IList>(DeleteCategoriesAction, selectedItems => {
                return !Context.ChangeTracker.HasChanges() && selectedItems?.Count > 0;
            });

            Register<string>(this, AppMessages.MSG_REFRESH_CATEGORIES, courseId =>
            {
                if (courseId == Course?.Id.ToString())
                    LoadCategories();
            });


        }

        private void LoadCategories()
        {

            Categories.RefreshFromModel(Category.GetCategories(CurrentUser, Course));

        }

        private void AddNewCategoryAction()
        {

            List<Category> test = CategoriesView.SourceCollection.Cast<Category>().ToList();
            Category.updateOrAddCategoriesInCourse(test, Course);
            NotifyColleagues(AppMessages.MSG_REFRESH_CATEGORIES, Course.Id.ToString());
        }

        private void DeleteCategoriesAction(IList categories)
        {

            var deleted = categories.Cast<Category>().ToArray();
            Categories.RemoveRange(deleted);
            Category.removeCategories(deleted);
            RaisePropertyChanged(nameof(Category));
            NotifyColleagues(AppMessages.MSG_REFRESH_CATEGORIES, Course.Id.ToString());
        }


        protected override void OnRefreshData()
        {
            LoadCategories();

        }
    }
}
