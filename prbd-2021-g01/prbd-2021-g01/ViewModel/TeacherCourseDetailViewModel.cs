using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using prbd_2021_g01.Model;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    public class TeacherCourseDetailViewModel : ViewModelCommon {

        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Delete { get; set; }

        private Course course;
        public Course Course { get => course; set => SetProperty(ref course, value); }

        private bool isNew;
        public bool IsNew {
            get { return isNew; }
            set {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew)); //, nameof(IsExisting));
            }
        }

        public bool IsExisting { get => !isNew; }

        public string Title {
            get { return Course?.Title; }
            set {
                Course.Title = value;
                RaisePropertyChanged(nameof(Title));
                NotifyColleagues(AppMessages.MSG_TITLE_CHANGED, Course);
            }
        }

        private string description;
        public string Description {
            get { return Course?.Description; }
            set  { // => SetProperty(ref description, value);
                Course.Description = value;
                RaisePropertyChanged(nameof(Description));
                }
        }

        public int MaximumCapacity {
            get {
                if (Course?.MaxStudent == null)
                    return 0;
                else
                    return Course.MaxStudent; }
            set {
                Course.MaxStudent = value;
                RaisePropertyChanged(nameof(MaximumCapacity));
            }
        }

        private string teacher;
        public string Teacher { 
            get { return CurrentUser.ToString(); }
            set => SetProperty(ref teacher, value);
        }
        //public string Teacher { get { return CurrentUser.ToString(); }  }

        public TeacherCourseDetailViewModel() : base() {
            Save = new RelayCommand(SaveAction, CanSaveAction);
            Cancel = new RelayCommand(CancelAction, CanCancelAction);
            Delete = new RelayCommand(DeleteAction, () => !IsNew);

        }

        public void Init(Course course, bool isNew) {
            // Bind properties of child ViewModel
            /*this.BindOneWay(nameof(Course), MemberMessages, nameof(MemberMessages.Member));*/

            // Il faut recharger ce membre dans le contexte courant pour pouvoir le modifier
            Course = course; // isNew ? course : Course.GetByTitle(course.Title);
            IsNew = isNew;

            //RaisePropertyChanged();
        }

        protected override void OnRefreshData() {
        }

        private void SaveAction() {
            if (IsNew) {
                // Un petit raccourci ;-)
                //Course.Title = Course.Title;
                //Course course = new Course();
                Context.Add(Course);
                IsNew = false;
            }
            Context.SaveChanges();
            NotifyColleagues(AppMessages.MSG_COURSE_CHANGED, Course);
        }

        private bool CanSaveAction() {
            if (IsNew)
                return !string.IsNullOrEmpty(Title);
            return Course != null && (Context?.Entry(Course)?.State == EntityState.Modified);
        }

        private void CancelAction() {
            

            //if (IsNew) {
            //    NotifyColleagues(AppMessages.MSG_CLOSE_TAB, Member);
            //} else {
            //    Context.Reload(Member);
            //    RaisePropertyChanged();
            //}
        }

        private bool CanCancelAction() {
            //return Member != null && (IsNew || Context?.Entry(Member)?.State == EntityState.Modified);
            return true;
        }

        private void DeleteAction() {
            //CancelAction();
            //if (File.Exists(PicturePath))
            //    File.Delete(PicturePath);
            //Member.Delete();
            //NotifyColleagues(AppMessages.MSG_MEMBER_CHANGED, Member);
            //NotifyColleagues(AppMessages.MSG_CLOSE_TAB, Member);
        }

        private ObservableCollection<Course> courses;
        public ObservableCollection<Course> Courses {
            get => courses;
            set => SetProperty<ObservableCollection<Course>>(ref courses, value);
        }

        

        /*private string description;

        public string Description { get => description; set => SetProperty(ref description, value); }

        private string teacher; // TODO: ask question about comment (what about readonly?)


        protected override void OnRefreshData() {

        }

        /*Messages = new ObservableCollectionFast<Message>(Message.GetAll());
           Members = new ObservableCollectionFast<Member>(Member.GetAll());

           DeleteCommand = new RelayCommand(() => {
               var recipientPseudo = SelectedMessage.Recipient.Pseudo;
               SelectedMessage.Delete();
               Messages.Remove(SelectedMessage);
               SelectedMessage = null;
               NotifyColleagues(AppMessages.MSG_REFRESH_MESSAGES, recipientPseudo);
           },
           () => ReadMode && SelectedMessage != null);

           SaveCommand = new RelayCommand(() => {
               if (SelectedMessage.MessageId == 0)
                   Context.Messages.Add(SelectedMessage);
               Context.SaveChanges();
               RaisePropertyChanged(nameof(MessagesView));
               RefreshGrid();
               EditMode = false;
               NotifyColleagues(AppMessages.MSG_REFRESH_MESSAGES, SelectedMessage.Recipient.Pseudo);
           },
           () => EditMode && Validate());

           CancelCommand = new RelayCommand(() => {
               Context.Entry(SelectedMessage).Reload();
               RaisePropertyChanged(nameof(MessagesView));
               RefreshGrid();
               EditMode = false;
               Validate();
               RaisePropertyChanged();
           },
           () => EditMode);*/

        /*RefreshCommand = new RelayCommand(() => {
            RefreshGrid();
        },
        () => ReadMode);

        NewCommand = new RelayCommand(() => {
            SelectedMessage = new Message();
            EditMode = true;
        },
        () => ReadMode);

        Register<String>(this, AppMessages.MSG_REFRESH_MESSAGES, _ => {
            RefreshGrid();
        });*/
    }

}
