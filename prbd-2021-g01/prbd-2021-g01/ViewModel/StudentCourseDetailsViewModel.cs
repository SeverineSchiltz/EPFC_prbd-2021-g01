using prbd_2021_g01.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prbd_2021_g01.ViewModel
{
    class StudentCourseDetailsViewModel : ViewModelCommon
    {

        private Course course;
        public Course Course { get => course; set => SetProperty(ref course, value); }

        public ICommand DisplayQuizCourseDetails { get; set; }

        private ObservableCollectionFast<Quiz> quizz = new ObservableCollectionFast<Quiz>();
        public ObservableCollectionFast<Quiz> Quizz
        {
            get => quizz;
            //set => SetProperty(ref quizz, value);
        }

        public string Title
        {
            get { return Course?.Title; }

        }

        public string Description
        {
            get { return Course?.Description; }

        }

        public string Teacher
        {
            get { return Course?.Teacher.ToString(); }


        }

        public ICollectionView QuizzView => Quizz.GetCollectionView(nameof(Quiz.EndDateTime), ListSortDirection.Descending);
        public StudentCourseDetailsViewModel()
        {
            DisplayQuizCourseDetails = new RelayCommand<Quiz>(quiz => {
                NotifyColleagues(AppMessages.MSG_DISPLAY_STUDENT_QUIZ, quiz);
            });
        }


        public void Init(Course course)
        {
            // Bind properties of child ViewModel
            //this.BindOneWay(nameof(Member), MemberMessages, nameof(MemberMessages.Member));

            Course = course;

            OnRefreshData();
        }

        protected override void OnRefreshData()
        {
            Quizz.Reset(Quiz.GetQuizzStudent(Course));
            RaisePropertyChanged();
            //throw new NotImplementedException();
        }
    }
}
