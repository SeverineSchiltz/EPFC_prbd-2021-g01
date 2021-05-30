using prbd_2021_g01.Model;
using PRBD_Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace prbd_2021_g01.ViewModel
{
    public class TeacherCourseQuizViewModel : ViewModelCommon
    {
        private ObservableCollectionFast<Quiz> quizzes = new ObservableCollectionFast<Quiz>();
        public ObservableCollectionFast<Quiz> Quizzes
        {
            get => quizzes;
            set => SetProperty(ref quizzes, value);
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

        public ICommand SaveQuizzes { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand DeleteQuizzes { get; set; }


        public ICollectionView QuizView => Quizzes.GetCollectionView(nameof(Quiz.Title), ListSortDirection.Descending);

        public TeacherCourseQuizViewModel()
        {

            //Course est à null...
            //Quizzes = new ObservableCollection<Quiz>(Quiz.GetQuizzes(CurrentUser, Course));
            //LoadQuizzes();

            Console.WriteLine("quiz vm constr");

            SaveQuizzes = new RelayCommand(AddNewQuizzesAction);

            Cancel = new RelayCommand(LoadQuizzes);

            DeleteQuizzes = new RelayCommand<IList>(DeleteQuizzesAction, selectedItems => {
                return !Context.ChangeTracker.HasChanges() && selectedItems?.Count > 0;
            });

            Register<string>(this, AppMessages.MSG_REFRESH_QUIZ, courseId =>
            {
                Console.WriteLine("quiz vm register");
                if (courseId == Course?.Id.ToString())
                    LoadQuizzes();
            });


        }

        private void LoadQuizzes()
        {
            Console.WriteLine("quiz vm load");
            Quizzes.Reset(Quiz.GetQuizzes(Course));
            RaisePropertyChanged(nameof(Quizzes));
        }

        private void AddNewQuizzesAction()
        {

            List<Quiz> listQuiz = QuizView.SourceCollection.Cast<Quiz>().ToList();
            Quiz.updateOrAddQuizzesInCourse(listQuiz, Course);
            NotifyColleagues(AppMessages.MSG_REFRESH_QUIZ, Course.Id.ToString());
        }

        private void DeleteQuizzesAction(IList quizzes)
        {
            bool isQuiz = true;
            for(int i = 0; i< quizzes.Count; ++i)
            {
                if (quizzes[i].GetType() != typeof(Quiz))
                {
                    isQuiz = false;
                }
            }
            if (isQuiz)
            {
                var deleted = quizzes.Cast<Quiz>().ToArray();
                Quizzes.RemoveRange(deleted);
                Quiz.removeQuizzes(deleted);
                //RaisePropertyChanged(nameof(Quizzes));
                NotifyColleagues(AppMessages.MSG_REFRESH_QUIZ, Course.Id.ToString());
            }

        }


        protected override void OnRefreshData()
        {
            Console.WriteLine("quiz vm refresh");
            LoadQuizzes();
            //RaisePropertyChanged(nameof(Quizzes));
        }
    }
}
