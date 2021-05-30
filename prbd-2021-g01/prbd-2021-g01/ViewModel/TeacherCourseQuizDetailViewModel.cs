using System;
using System.Linq;
using PRBD_Framework;
using System.Windows.Input;
using prbd_2021_g01.Model;
using System.Collections;
using System.ComponentModel;

namespace prbd_2021_g01.ViewModel {
    public class TeacherCourseQuizDetailViewModel : ViewModelCommon
    {
        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set
            {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew), nameof(IsExisting));
            }
        }

        public bool IsExisting { get => !isNew; }

        private Quiz quiz;

        public Quiz Quiz { get => quiz; set => SetProperty(ref quiz, value); }

        public void Init(Quiz quiz, bool isNew)
        {

            // Il faut recharger ce quiz dans le contexte courant pour pouvoir le modifier
            //Quiz = isNew ? quiz : Quiz.GetById(quiz.Id);
            Quiz = quiz;
            IsNew = isNew;
            if(!isNew)
            {
                Title = quiz.Title;
                Start = quiz.StartDateTime;
                End = quiz.EndDateTime;
                QuizQuestions = new ObservableCollectionFast<QuizQuestion>(quiz.Questions);
                IList questions = new ArrayList();
                foreach (var qq in quiz.Questions)
                    questions.Add(qq.Question);
                OtherQuestions = new ObservableCollectionFast<Question>(Question.GetQuestionsExcept(questions, course));
            }

            RaisePropertyChanged();
        }
        public ICollectionView QuizQuestionsView => QuizQuestions.GetCollectionView(nameof(Quiz.Title), ListSortDirection.Descending);
        public ICollectionView OtherQuestionsView => OtherQuestions.GetCollectionView(nameof(Quiz.Title), ListSortDirection.Descending);

        private ObservableCollectionFast<Question> otherQuestions;
        public ObservableCollectionFast<Question> OtherQuestions {
            get => otherQuestions;
            set => SetProperty(ref otherQuestions, value);
        }

        private ObservableCollectionFast<QuizQuestion> quizQuestions;
        public ObservableCollectionFast<QuizQuestion> QuizQuestions
        {
            get => quizQuestions;
            set => SetProperty(ref quizQuestions, value);
        }

        private string title;
        public string Title { get => title; set => SetProperty(ref title, value, () => Validate()); }

        private DateTime? start;
        public DateTime? Start { get => start; set => SetProperty(ref start, value, () => Validate()); }

        private DateTime? end;
        public DateTime? End { get => end; set => SetProperty(ref end, value, () => Validate()); }

        private string filter;
        public string Filter {
            get => filter;
            set => SetProperty<string>(ref filter, value, ApplyFilterAction);
        }

        private void ApplyFilterAction() {
            var categories = from cat in Category.GetCategories(course)
                             where cat.Title.Contains(filter)
                             select cat;

            var query = from q in Question.GetAllQuestions()
                        where q.Categories.Intersect(categories).Count() > 0 //vérifier les éléments communs entre les 2 listes
                        select q;

            otherQuestions = new ObservableCollectionFast<Question>(query);
        }

        private Course course;
        public Course Course {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        private IList selectedOtherQuestions = new ArrayList();
        public IList SelectedOtherQuestions
        {
            get => selectedOtherQuestions;
            set => SetProperty(ref selectedOtherQuestions, value);
        }

        private IList selectedQuizQuestions = new ArrayList();
        public IList SelectedQuizQuestions
        {
            get => selectedQuizQuestions;
            set => SetProperty(ref selectedQuizQuestions, value);
        }

        public ICommand AddQuestions { get; set; }
        public ICommand RemoveQuestions { get; set; }
        public ICommand ClearFilter { get; set; }

        public TeacherCourseQuizDetailViewModel() : base() {
            AddQuestions = new RelayCommand(AddQuestionsAction);
            RemoveQuestions = new RelayCommand(RemoveQuestionsAction);
            ClearFilter = new RelayCommand(() => Filter = "");
        }

        protected override void OnRefreshData() {
            OtherQuestions = new ObservableCollectionFast<Question>(Question.GetAllQuestions());
        }

        public void AddQuestionsAction() {
            quiz.addQuestions((System.Collections.Generic.IList<Question>)selectedOtherQuestions);
            ResetAndNotify();
        }

        public void RemoveQuestionsAction() {
            quiz.removeQuestions(selectedQuizQuestions);
            ResetAndNotify();
        }

        private void ResetAndNotify() {
            OtherQuestions.Reset(Question.GetAllQuestions());
            RaisePropertyChanged();
            NotifyColleagues(AppMessages.MSG_QUIZ_CHANGED);
        }
    }
}
