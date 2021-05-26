using prbd_2021_g01.Model;
using PRBD_Framework;
using System;
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
        public event Action UnSelect;
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

        
        public ICommand AllCategory { get; set; }
        public ICommand NoneCategory { get; set; }
        public ICommand CheckCategory { get; set; }
        public ICommand NewQuestion { get; set; }
        public ICommand SaveQuestion { get; set; }
        public ICommand CancelQuestion { get; set; }
        public ICommand DeleteQuestion { get; set; }


        public TeacherCourseQuestionsViewModel()
        {
            //Console.WriteLine("TeacherCourseQuestionsViewModel");

            Register<string>(this, AppMessages.MSG_REFRESH_CATEGORIES, courseId =>
            {
                if (courseId == Course?.Id.ToString())
                    OnRefreshData();
            });

            CheckCategory = new RelayCommand(loadQuestions);
            AllCategory = new RelayCommand(allCategoriesAction);

            NoneCategory = new RelayCommand(noneCategoriesAction);

            NewQuestion = new RelayCommand(AddNewQuestionAction);

            SaveQuestion = new RelayCommand<Question>(SaveQuestionAction, question =>
            {
                return SelectedItem != null;
            });

            CancelQuestion = new RelayCommand(OnRefreshData);

            DeleteQuestion = new RelayCommand<Question>(DeleteQuestionAction, question =>
            {
                return SelectedItem != null;
            });
        }


        protected void loadQuestions()
        {
            //var test = Category.GetCategories(CurrentUser, Course);
            //List<Category> listCat = CategoriesView.SourceCollection.Cast<Category>().ToList();
            //var query = Question.GetQuestions(Course, listCat);
            Questions.Reset(Question.GetQuestions(Course)); //, listCat));
            //Questions = new ObservableCollectionFast<Question>(query);
            RaisePropertyChanged();
        }

        protected void allCategoriesAction()
        {
            foreach(Category c in Categories)
            {
                c.select();
            }
            loadQuestions();
            RaisePropertyChanged(nameof(CategoriesView));
            
        }

        protected void noneCategoriesAction()
        {
            foreach (Category c in Categories)
            {
                c.unSelect();
            }
            loadQuestions();
            RaisePropertyChanged(nameof(CategoriesView));
        }


        public void AddNewQuestionAction()
        {
            SelectedItem = new Question(Course, "", "");

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
            Questions.Reset(Question.GetQuestions(Course)); //, listCat));
            RaisePropertyChanged(); //refresh toutes les propriétés du viewmodel dans la vue
            //RaisePropertyChanged(nameof(Categories));
            //throw new NotImplementedException();
        }
    }
}
