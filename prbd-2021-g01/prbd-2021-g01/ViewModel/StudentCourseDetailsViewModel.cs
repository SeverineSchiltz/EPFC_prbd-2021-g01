using prbd_2021_g01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.ViewModel
{
    class StudentCourseDetailsViewModel : ViewModelCommon
    {

        private Course course;
        public Course Course { get => course; set => SetProperty(ref course, value); }
        protected override void OnRefreshData()
        {
            throw new NotImplementedException();
        }

        public string Title
        {
            get { return Course?.Title; }

        }

        public void Init(Course course)
        {
            // Bind properties of child ViewModel
            //this.BindOneWay(nameof(Member), MemberMessages, nameof(MemberMessages.Member));

            Course = course;

            RaisePropertyChanged();
        }
    }
}
