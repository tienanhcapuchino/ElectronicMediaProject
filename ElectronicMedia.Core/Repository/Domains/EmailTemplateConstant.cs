/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using ElectronicMedia.Core.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Repository.Domains
{
    public static class EmailTemplateNameConstant
    {
        public const string PostPublishedName = "Post published (built-in)";
        public const string CommentNotificationName = "Comment notification (built-in)";
        public const string PendingForApprovalPostName = "Pending for approval post (built-in)";
    }
    public class EmailTemplateTypeConstant
    {
        public const string PostPublished = "Post published";
        public const string CommentNotification = "Comment notification";
        public const string PendingForApprovalPost = "Pending for approval post";
    }
    public class EmailTemplateIdConstant
    {
        public const string PostPublishedId = "da894852-0c22-4f97-831f-996947584d91";
        public const string CommentNotificationId = "6ab21b15-d166-45ec-9f26-865c5150ed76";
        public const string PendingForApprovalPostId = "8a4e789f-d1ab-4661-8a97-7492351f3e67";
        public const string SystemAccountId = "e4adc524-90d6-4903-a7fa-0668fda5157e";
    }
    public class EmailTemplateBodyConstant
    {
        public const string PostPublishedBody = "Your post have been published! Let go to our website to view that!";
        public const string CommentNotificationBody = "Your following post have new comment! Let go to our website to see that!";
        public const string PendingForApprovalPostBody = "You have a new post that need to be approved! Please go to website to check that!";
    }
    public class EmailTemplateSubjectConstant
    {
        public const string PostPublishedSubject = "Your post have been published";
        public const string CommentNotificationSubject = "Your post have a new comment";
        public const string PendingForApprovalPostSubject = "You have a post need to be approved";
    }
    public class EmailTemplateMailToConstant
    {
        public const string Admin = "$Admin";
        public const string Writer = "$Writer";
        public const string Leader = "$Leader";
        public const string EditorDirector = "$EditorDirector";
        public const string Participant = "$Participant";
    }
    public class EmailTemplateRecieverConstant
    {
        public const string PostPublishedReciever = EmailTemplateMailToConstant.Writer;
        public const string CommentNotificationReciever = EmailTemplateMailToConstant.Participant;
        public const string PendingForApprovalPostReciever = EmailTemplateMailToConstant.Leader;
    }
    public class EmailTemplateDescriptionConstant
    {
        public const string PostPublishedDes = "Notify to writer that your post is published!";
        public const string CommentNotificationDes = "Notify to participant in the post that the post have new comment";
        public const string PendingForApprovalPostDes = "Notify to leader that he (she) has a new post need to be approved";
    }
    public static class EmailTemplateConstants
    {
        public static List<EmailTemplate> GetAllEmailTemplateBuitIns()
        {
            List<EmailTemplate> emailTemplates = new List<EmailTemplate>()
            {
                new EmailTemplate()
                {
                    Id = new Guid(EmailTemplateIdConstant.PostPublishedId),
                    Name = EmailTemplateNameConstant.PostPublishedName,
                    Subject = EmailTemplateSubjectConstant.PostPublishedSubject,
                    Body = EmailTemplateBodyConstant.PostPublishedBody,
                    MailType = MailType.PostPublished,
                    Description = EmailTemplateDescriptionConstant.PostPublishedDes,
                    MailTo = EmailTemplateRecieverConstant.PostPublishedReciever,
                    IsUsed = false,
                    ModifiedBy = EmailTemplateIdConstant.SystemAccountId,
                },
                new EmailTemplate()
                {
                    Id = new Guid(EmailTemplateIdConstant.CommentNotificationId),
                    Name = EmailTemplateNameConstant.CommentNotificationName,
                    Subject = EmailTemplateSubjectConstant.CommentNotificationSubject,
                    Body = EmailTemplateBodyConstant.CommentNotificationBody,
                    MailType = MailType.CommentNotification,
                    Description = EmailTemplateDescriptionConstant.CommentNotificationDes,
                    MailTo = EmailTemplateRecieverConstant.CommentNotificationReciever,
                    IsUsed = false,
                    ModifiedBy = EmailTemplateIdConstant.SystemAccountId,
                },
                new EmailTemplate()
                {
                    Id = new Guid(EmailTemplateIdConstant.PendingForApprovalPostId),
                    Name = EmailTemplateNameConstant.PendingForApprovalPostName,
                    Subject = EmailTemplateSubjectConstant.PendingForApprovalPostSubject,
                    Body = EmailTemplateBodyConstant.PendingForApprovalPostBody,
                    MailType = MailType.PendingForApprovalPost,
                    Description = EmailTemplateDescriptionConstant.PendingForApprovalPostDes,
                    MailTo = EmailTemplateRecieverConstant.PendingForApprovalPostReciever,
                    IsUsed = false,
                    ModifiedBy = EmailTemplateIdConstant.SystemAccountId,
                }
            };
            return emailTemplates;
        }
    }
}
