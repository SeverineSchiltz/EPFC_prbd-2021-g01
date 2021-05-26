using prbd_2021_g01.Model;
using PRBD_Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

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
        private ObservableCollectionFast<Question> questions = new ObservableCollectionFast<Question>();
        public ObservableCollectionFast<Question> Questions
        {
            get => questions;
            set => SetProperty(ref questions, value);
        }

        public ICollectionView CategoriesView => Categories.GetCollectionView(nameof(Category.Title), ListSortDirection.Descending);
        public ICollectionView QuestionsView => Questions.GetCollectionView(nameof(Question.Title), ListSortDirection.Ascending);

        private Course course;
        public Course Course
        {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        private Question selectedItem = new Question();
        public Question SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value, RaisePropertyChanged);
        }

        private ListBox listb;


        //public ListBox Listb
        //{
        //    get => selectedItem;
        //    set => SetProperty(ref selectedItem, value, RaisePropertyChanged);
        //}

        public string Title
        {
            get { return selectedItem?.Title; }
            set
            {
                selectedItem.Title = value;
                RaisePropertyChanged();
                //NotifyColleagues(AppMessages.MSG_TITLE_CHANGED, Course);
            }
        }
        public string AnswersText
        {
            get { return selectedItem?.GetAnswersAsString(); }
            set
            {
                //selectedItem.Answers = value;
                RaisePropertyChanged();
                //NotifyColleagues(AppMessages.MSG_TITLE_CHANGED, Course);
            }
        }

        public ICommand CheckBox_Click { get; set; }
        public ICommand DisplayQuestion { get; set; }

        public ICommand NewQuestion { get; set; }
        public ICommand SaveQuestion { get; set; }
        public ICommand CancelQuestion { get; set; }
        public ICommand DeleteQuestion { get; set; }


        public TeacherCourseQuestionsViewModel()
        {
            Register<string>(this, AppMessages.MSG_REFRESH_CATEGORIES, courseId =>
            {
                if (courseId == Course?.Id.ToString())
                    OnRefreshData();
            });

            CheckBox_Click = new RelayCommand(loadQuestions);

            DisplayQuestion = new RelayCommand<ListBox>(displayQuestionInRightScreen, listB => {
                return true;
            });

            NewQuestion = new RelayCommand(AddNewQuestionAction);

            SaveQuestion = new RelayCommand<Question>(SaveQuestionAction, question =>
            {
                return SelectedItem.Title != null;
            });

            CancelQuestion = new RelayCommand(OnRefreshData);

            DeleteQuestion = new RelayCommand<Question>(DeleteQuestionAction, question =>
            {
                return SelectedItem.Title != null;
            });
        }


        protected void loadQuestions()
        {
            var test = Category.GetCategories(CurrentUser, Course);
            List<Category> listCat = CategoriesView.SourceCollection.Cast<Category>().ToList();
            var query = Question.GetQuestions(Course, listCat);
            Questions = new ObservableCollectionFast<Question>(query);

        }

        protected void displayQuestionInRightScreen(ListBox listbox)
        {
            listb = listbox;

        }

        public void AddNewQuestionAction()
        {
            //SelectedItem = new Question(Course, "", "");
            listb.SelectedIndex = -1;
            RaisePropertyChanged();
        }
        public void SaveQuestionAction(Question q)
        {

        }
        public void DeleteQuestionAction(Question q)
        {

        }



        protected override void OnRefreshData()
        {
            Categories.Reset(Category.GetCategories(CurrentUser, Course));
            List<Category> listCat = CategoriesView.SourceCollection.Cast<Category>().ToList();
            Questions.Reset(Question.GetQuestions(Course, listCat));
            RaisePropertyChanged(); //refresh toutes les propriétés du viewmodel dans la vue
            //RaisePropertyChanged(nameof(Categories));
            //throw new NotImplementedException();
        }
    }
}
