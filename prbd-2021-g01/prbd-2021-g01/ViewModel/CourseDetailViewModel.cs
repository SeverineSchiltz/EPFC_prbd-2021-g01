using System;
using System.IO;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using prbd_2021_g01.Model;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    public class CourseDetailViewModel : ViewModelCommon {

        private Course course;
        public Course Course { get => course; set => SetProperty(ref course, value); }

        private bool isNew;
        public bool IsNew {
            get { return isNew; }
            set {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew), nameof(IsExisting));
            }
        }

        public bool IsExisting { get => !isNew; }

        public void Init(Course course, bool isNew) {
            /*// Bind properties of child ViewModel
            this.BindOneWay(nameof(Member), MemberMessages, nameof(MemberMessages.Member));*/

            // Il faut recharger ce membre dans le contexte courant pour pouvoir le modifier
            Course = isNew ? course : Course.GetByTitle(course.Title);
            IsNew = isNew;

            RaisePropertyChanged();
        }

        protected override void OnRefreshData() {
            
        }
    }
}
