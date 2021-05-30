using prbd_2021_g01.Model;
using PRBD_Framework;
using System;
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
        private ObservableCollectionFast<Category> categories = new ObservableCollectionFast<Category>();
        public ObservableCollectionFast<Category> Categories
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
            foreach(Category c in Categories)
            {
                Context.Reload(c);
            }
            Categories.Reset(Category.GetCategories(Course));
            RaisePropertyChanged(nameof(Categories));
        }

        private void AddNewCategoryAction()
        {

            List<Category> listCat = CategoriesView.SourceCollection.Cast<Category>().ToList();
            Category.updateOrAddCategoriesInCourse(listCat, Course);
            NotifyColleagues(AppMessages.MSG_REFRESH_CATEGORIES, Course.Id.ToString());
        }

        private void DeleteCategoriesAction(IList categories)
        {
            bool isCat = true;
            for(int i = 0; i< categories.Count; ++i)
            {
                if (categories[i].GetType() != typeof(Category))
                {
                    isCat = false;
                }
            }
            if (isCat)
            {
                var deleted = categories.Cast<Category>().ToArray();
                Categories.RemoveRange(deleted);
                Category.removeCategories(deleted);
                //RaisePropertyChanged(nameof(Categories));
                NotifyColleagues(AppMessages.MSG_REFRESH_CATEGORIES, Course.Id.ToString());
            }

        }


        protected override void OnRefreshData()
        {
            LoadCategories();
            //RaisePropertyChanged(nameof(Categories));
        }
    }
}
