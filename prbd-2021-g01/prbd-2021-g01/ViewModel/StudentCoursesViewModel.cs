using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PRBD_Framework;
using System.Collections.Generic;
using prbd_2021_g01.Model;

namespace prbd_2021_g01.ViewModel
{
    public class StudentCoursesViewModel : ViewModelCommon
    {

        private ObservableCollection<Course> courses;
        public ObservableCollection<Course> Courses
        {
            get => courses;
            set => SetProperty<ObservableCollection<Course>>(ref courses, value);
        }

        private string title;

        public string Title { get => title; set => SetProperty(ref title, value); }

        private string description;

        public string Description { get => description; set => SetProperty(ref description, value); }

        private string teacher; // TODO: ask question about comment (what about readonly?)

        public string Teacher { get { return teacher.ToString(); } set => SetProperty(ref description, value); }

        private string filter;
        public string Filter {
            get => filter;
            set => SetProperty<string>(ref filter, value, ApplyFilterAction);
        }
        private void ApplyFilterAction() {
            var query = from c in Context.Courses where
                        c.Title.Contains(Filter) || c.Description.Contains(Filter) select c;
            Courses = new ObservableCollection<Course>(query);
        }

        public ICommand ClearFilter { get; set; }

        public StudentCoursesViewModel() : base()
        {
            Courses = new ObservableCollection<Course>(App.Context.Courses);

            ClearFilter = new RelayCommand(() => Filter = "");

            //AllSelected = true;
        }

        protected override void OnRefreshData()
        {

        }

    }
}
