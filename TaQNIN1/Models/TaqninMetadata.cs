using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaQNIN1.Models
{
    public class TaqninMetadata
    {
        [Key]
        public int id { get; set; }
        public string uploaddate { get; set; }
        public string person_upload { get; set; }
        public string geographic_person { get; set; }
        public string geographic_person_response { get; set; }
        public string responsedate { get; set; }
        public string studentUser { get; set; }
        public string studynotes { get; set; }
        public string captianUser { get; set; }
        public string notes_captain { get; set; }
        public string PoineerUser { get; set; }
        public string PoineerNotes { get; set; }
        public string majorUser { get; set; }
        public string responseUser { get; set; }

        public string Descion223 { get; set; }
        public string DescionQM { get; set; }
        //رفع مساحي
        public string RaiseSurveyors { get; set; }
        //مستوفي للضوابط والشروط
        public string fullfilterms { get; set; }
        //مؤجل
        public string Delayed { get; set; }
        //المساحه الكليه
        public double Fullarea { get; set; }
        ////not used
        //public string geographic_descion { get; set; }
        //المساحه المتبقيه
        public double Remainingspace { get; set; }
        //المركز الجغرافي موافق او غير موافق
        public string AcceptOrNot { get; set; }
        public double Convertedspace { get; set; }

        //تداخل بعد النطاق
        public string Overlap_after_range { get; set; }
        //قرار مركز المتغيرات
        public string ChangesCenterDescion { get; set; }
        //تداخل ق.م-قرار223
        public bool D223AndQM { get; set; }

        public bool landspace { get; set; }
        public string Imagedate { get; set; }
        public string PreviewDate { get; set; }
        //وحده قياس المساحه من محضر المعاينه
        public string InspectionMeasure  { get; set; }
        //ملاحظات القائم بالمراجعه
        public string ReviewerNotes { get; set; }
        //ملاحظات رئيس قسم استرداد أراضي الدولة
        public string RecoveryDepartmentNotes { get; set; }
        //ملاحظات قائد مركز المتغيرات
        public string VariablesCommanderNotes { get; set; }
        //ملاحظات السيد مدير ادارة المساحة العسكريه
        public string  MilitarySurveyDepartmentNotes { get; set; }
        //ملاحظات الرد
        public string ResponseNotes { get; set; }
        //طلب مؤجل
        public bool SuspendedOrder { get; set; }
        //قانونيه مستوفي
        public string LegalFullfied { get; set; }
        //المساحه
        public string AreaFullfied { get; set; }
        //غير مدقق
        public bool NotauditedActivity { get; set; }
        //تدقيق المساحه
        public bool Spaceaudit { get; set; }
        //تدقيق الرفع المساحي
        public bool Surveyingliftauditing { get; set; }
        //عدم إدخال الاحداثيات طبقاً لمحضر المعاينة
        public bool NotEnteringCoordinates  { get; set; }
        //عدم انطباق الاحداثيات مع وصف محضر المعاينة/ مع وصف الكروكي
        public bool NotApplicableCoordinates { get; set; }
        //لا يوجد مرفقات
        public bool NoAttachments { get; set; }
        //اعتماد المراجع
        public string ReviewerApproval { get; set; }
        //flag betwen study and review true back false from review to study
        public bool ReviewFlag { get; set; }

        //اعتماد النقيب
      
         public string CaptianApproval { get; set; }
        //flag betwen study and review true and captian back false 
        public bool captianFlag { get; set; }
        public string Reviewer { get; set; }
        public string PoineerApproval { get; set; }
        public string MajorApproval { get; set; }
        public int  BackToGeographic { get; set; }
        public int BackToVariableCenter { get; set; }
        public int BackToReviewer { get; set; }
        public int BackToCaptian { get; set; }
        public int BackToMajor { get; set; }
        public string suspendedBy { get; set; }
        public int BackToPoineer { get; set; }
        public string MajorNotes { get; set; }
        public int ImageBeforeCounter { get; set; }
        public int ImageAfterCounter { get; set; }
        public string ResponseApproval { get; set; }
        public int BackToresponse { get; set; }
        public string OrderStatus { get; set; }

        public string honesty { get; set; }
        public string Domain { get; set; }
        public virtual TaqninData TaqninData { get; set; }
        public int Taqninid { get; set; }
        public string responsestatus { get; set; }

        public string ReviewercheckApproval { get; set; }
        public int BackToReviewercheck { get; set; }
        public string ResponseSuspended { get; set; }

        public string ReviewerSuspended { get; set; }
    }
}