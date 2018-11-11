using System.Web.Configuration;

namespace Erp.Utilities
{
    public sealed class Globals
    {
        private Globals() { }

        #region Static Members

        public static string UserTypeNameForFSM = WebConfigurationManager.AppSettings["UserTypeNameForFSM"] ?? "Trainer";

        public static string PositionCodeFSM = WebConfigurationManager.AppSettings["PositionCodeFSM"] ?? "FSM";

        public static string ImagePath = WebConfigurationManager.AppSettings["ImagePath"] ?? "~/Images/";

        public static string UploadedFilePath = WebConfigurationManager.AppSettings["UploadedFilePath"] ?? "~/UploadedFile/";

        public static string QuizImageDefault = WebConfigurationManager.AppSettings["QuizImageDefault"] ?? "~/Images/quiz.jpg";

        public static string FrontEndUrl = WebConfigurationManager.AppSettings["FrontEndUrl"] ?? "http://localhost:5320/";

        public static string BackEndUrl = WebConfigurationManager.AppSettings["BackEndUrl"] ?? "http://localhost:5319/";

        public static string ForumUrl = WebConfigurationManager.AppSettings["ForumUrl"] ?? "http://localhost:15703/";

        public static string NoImageDefaultPath = WebConfigurationManager.AppSettings["NoImageDefaultPath"] ?? "/Images/NoImage.jpg";

        public static string maxRequestLength = WebConfigurationManager.AppSettings["maxRequestLength"] ?? "52428800";

        public static int PageSizeFo = 8;

        public const float TestConditionForBluePoint = 85;

        public const string SuccessMessageKey = "SuccessMessage";
        public const string FailedMessageKey = "FailedMessage";
        public const string Message = "Message";

        #endregion Static Members

        #region Enum
        
        /// <summary>
        /// for Lesson Slide
        /// </summary>
        public enum SlideType
        {
            /// <summary>
            /// The image.
            /// </summary>
            Image = 0,

            /// <summary>
            /// The quiz.
            /// </summary>
            Quiz = 1
        }

        /// <summary>
        /// The lesson learning status.
        /// </summary>
        public enum LessonLearningStatus
        {
            /// <summary>
            /// The not yet.
            /// </summary>
            NotYet = 0,

            /// <summary>
            /// The learning.
            /// </summary>
            Learning,

            /// <summary>
            /// The finished.
            /// </summary>
            Finished,
        }

        /// <summary>
        /// The quiz question type.
        /// </summary>
        public enum QuizQuestionType
        {
            /// <summary>
            /// The text.
            /// </summary>
            Text = 0,

            /// <summary>
            /// The image.
            /// </summary>
            Image = 1,

            /// <summary>
            /// The video.
            /// </summary>
            Video = 2,

            /// <summary>
            /// The images mapping.
            /// </summary>
            ImagesMapping = 3
        }

        /// <summary>
        /// The training schedule type.
        /// </summary>
        public enum TrainingScheduleType
        {
            /// <summary>
            /// The lesson.
            /// </summary>
            Weekly = 0,

            /// <summary>
            /// The test.
            /// </summary>
            Other = 1,

            /// <summary>
            /// The learning schedule.
            /// </summary>
            LearningSchedule = 2
        }

        /// <summary>
        /// The test status.
        /// </summary>
        public enum TestStatus
        {
            /// <summary>
            /// The testing.
            /// </summary>
            Testing = 0,

            /// <summary>
            /// The finished.
            /// </summary>
            Finished = 1
        }

        /// <summary>
        /// The lottery action type
        /// </summary>
        public enum LotteryActionType
        {
            All = 0,
            Auto = 1,
            Manual = 2
        }

        /// <summary>
        /// The lottery action.
        /// </summary>
        public enum LotteryAction
        {
            /// <summary>
            /// No action
            /// </summary>
            None = 0,
            /// <summary>
            /// The account registration.
            /// </summary>
            AccountRegistration = 1,

            /// <summary>
            /// The start learning lesson.
            /// </summary>
            StartLearningLesson = 2,

            /// <summary>
            /// The lesson access 5 slide.
            /// </summary>
            LessonAccess5Slide = 3,

            /// <summary>
            /// The learning lesson complete.
            /// </summary>
            LearningLessonComplete = 4,

            /// <summary>
            /// The test result over 85 percent.
            /// </summary>
            TestResultOver85Percent = 5,

            /// <summary>
            /// The on class training date.
            /// </summary>
            OnClassTraining = 6,

            ///// <summary>
            ///// The on class training result.
            ///// </summary>
            //OnclassTrainingResult = 7,

            /// <summary>
            /// The onsite training date.
            /// </summary>
            OnsiteTraining = 8,

            ///// <summary>
            ///// The onsite training result.
            ///// </summary>
            //OnsiteTrainingResult = 9,

            /// <summary>
            /// The fresh.
            /// </summary>
            Fresh = 10,

            /// <summary>
            /// The associate.
            /// </summary>
            Associate = 11,

            /// <summary>
            /// The specialist.
            /// </summary>
            Specialist = 12,

            /// <summary>
            /// The expert.
            /// </summary>
            Expert = 13
        }

        /// <summary>
        /// The level.
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// The fresh.
            /// </summary>
            Fresh = 0,

            /// <summary>
            /// The associate.
            /// </summary>
            Associate = 1,

            /// <summary>
            /// The specialist.
            /// </summary>
            Specialist = 2,

            /// <summary>
            /// The expert.
            /// </summary>
            Expert = 3
        }

        #endregion Enum
    }
}
