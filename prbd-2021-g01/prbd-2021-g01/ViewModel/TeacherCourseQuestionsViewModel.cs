using prbd_2021_g01.Model;
using PRBD_Framework;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace prbd_2021_g01.ViewModel
{
    public class TeacherCourseQuestionsViewModel : ViewModelCommon
    {
        private ObservableCollectionFast<Category> categories = new ObservableCollectionFast<Category>();
        public ObservableCollectionFast<Category> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }
        public ICollectionView CategoriesView => Categories.GetCollectionView(nameof(Category.Title), ListSortDirection.Descending);


        private Course course;
        public Course Course
        {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        public TeacherCourseQuestionsViewModel()
        {
            Register<string>(this, AppMessages.MSG_REFRESH_CATEGORIES, courseId =>
            {
                if (courseId == Course?.Id.ToString())
                    OnRefreshData();
            });
        }


        protected override void OnRefreshData()
        {
            Categories.Reset(Category.GetCategories(CurrentUser, Course));
            RaisePropertyChanged(nameof(Categories));
            //throw new NotImplementedException();
        }
    }
}
