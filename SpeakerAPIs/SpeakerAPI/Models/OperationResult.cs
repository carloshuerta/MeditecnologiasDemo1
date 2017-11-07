using System;

namespace SpeakerAPI.Models
{
    public class OperationResult
    {
        /// <summary>
        /// The status of the operation
        /// </summary>
        public OperationStatus status { get; set; }

        /// <summary>
        /// Created date of the operation.
        /// </summary>
        public DateTime createdDateTime { get; set; }

        /// <summary>
        /// Last date of usage for this operation.
        /// </summary>
        public DateTime lastActionDateTime { get; set; }

        /// <summary>
        /// Detail message returned by this operation. Used in operations with failed status to show detail failure message.
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// An Json Object contains the processing result. This object exists only when the operation status is succeeded.
        /// </summary>
        public ProcessingResult processingResult { get; set; }

        /// <summary>
        /// Speaker identification profile enrollment status
        /// </summary>
        public EnrollmentStatus EnrollmentStatus { get; set; }

        /// <summary>
        /// Speaker identification profile enrollment length in seconds of speech.
        /// </summary>
        public int enrollmentSpeechTime { get; set; }

        /// <summary>
        /// Remaining number of speech seconds to complete minimum enrollment.
        /// </summary>
        public int remainingEnrollmentSpeechTime { get; set; }

        /// <summary>
        /// Seconds of useful speech in enrollment audio.
        /// </summary>
        public int speechTime { get; set; }
    }

    public class ProcessingResult
    {
        /// <summary>
        /// The identified speaker identification profile id. If this value is 00000000-0000-0000-0000-000000000000, it means there's no speaker identification profile identified and the audio file to be identified belongs to none of the provided speaker identification profiles.
        /// </summary>
        public string identifiedProfileId { get; set; }

        /// <summary>
        /// The confidence value of the identification.
        /// </summary>
        public IdentificationConfidence confidence { get; set; }
    }

    public enum OperationStatus
    {
        notstarted, // The operation is not started.
        running,    // The operation is running.
        failed,     // The operation is finished and failed.
        succeeded   // The operation is finished and succeeded.
    }

    public enum EnrollmentStatus
    {
        None,
        Enrolling,  // profile is currently enrolling and is not ready for identification.
        Training,   // profile is currently training and is not ready for identification.
        Enrolled    // profile is currently enrolled and is ready for identification.
    }

    public enum IdentificationConfidence
    {
        None,
        Low,        // The confidence of the identification is low.
        Normal,     // The confidence of the identification is normal.
        High        // The confidence of the identification is high.
    }
}