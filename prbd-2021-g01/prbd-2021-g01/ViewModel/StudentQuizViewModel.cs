using prbd_2021_g01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.ViewModel
{
    public class StudentQuizViewModel : ViewModelCommon
    {
        private Quiz quiz;
        public Quiz Quiz { get => quiz; set => SetProperty(ref quiz, value); }
        public void Init(Quiz quiz)
        {
            // Bind properties of child ViewModel
            //this.BindOneWay(nameof(Member), MemberMessages, nameof(MemberMessages.Member));

            Quiz = quiz;

            OnRefreshData();
        }

        protected override void OnRefreshData()
        {

        }
    }
}
