using prbd_2021_g01.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.ViewModel
{
    public class TeacherCourseQuizzesViewModel : ViewModelCommon
    {
        public ICollectionView QuizzView => Quizz.GetCollectionView(nameof(Quiz.EndDateTime), ListSortDirection.Descending);


        private ObservableCollectionFast<Quiz> quizz = new ObservableCollectionFast<Quiz>();
        public ObservableCollectionFast<Quiz> Quizz
        {
            get => quizz;
            //set => SetProperty(ref quizz, value);
        }

        private Course course;
        public Course Course
        {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        protected override void OnRefreshData()
        {
            Quizz.Reset(Quiz.GetQuizzTeacher(Course));
            //throw new NotImplementedException();
        }
    }
}
