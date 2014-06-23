using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JournalClub.ViewModels
{
    class PicuAttachmentViewModel : ObservableObject
    {
        public PicuAttachment PicuAttachment { get; set; }
        public DateTime StartDate
        {
            get { return PicuAttachment.StartDate; }
            set
            {
                var newDate = value.Date;
                if (PicuAttachment.StartDate != newDate)
                {
                    PicuAttachment.StartDate = newDate;
                    this.RaisePropertyChanged("StartDate");
                }
            }
        }
        public DateTime? FinishDate
        {
            get { return PicuAttachment.FinishDate; }
            set
            {
                var newDate = value.HasValue ? value.Value.Date : value;
                if (PicuAttachment.FinishDate != newDate)
                {
                    PicuAttachment.FinishDate = newDate;
                    this.RaisePropertyChanged("FinishDate");
                }
            }
        }
    }
}
