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
        public const string AddNewUserBody = @"<p><strong><em>Welcome to Electronic Media Online Service</em></strong></p>
                                            <p><strong><em>&nbsp;</em></strong></p>
                                            <p>You have been added to Electronic Media Online Service, below is your account&lsquo;s information.</p>
                                            <p><span style=""color: #993300;"">Email</span>: <a href=""{0}"">{1}</a></p>
                                            <p><span style=""color: #993300;"">Username</span>: <span style=""background-color: #ff9900;"">{2}</span></p>
                                            <p><span style=""color: #993300;"">Password</span>: <span style=""color: #0000ff;""><em><u>{3} </u></em></span></p>
                                            <p><span style=""color: #993300;"">Your role</span>: <span style=""background-color: #ff9900;"">{4}</span></p>
                                            <p>&nbsp;Please login to the website with your <strong><em>Username</em></strong> and <em><strong>Password</strong></em></p>";

        public const string DeletePostBody = @"<p><strong><em>Electronic Media Online Service Notification For Writer</em></strong></p>
                                            <p><span style=""color: #ff0000;"">Your post have been deleted by <strong>{0} : {1}</strong>. Below is the post information:</span></p>
                                            <p><strong>Title:</strong> {2}</p>
                                            <p>Reason for deleting post:</p>
                                            <p><em>{3}</em></p>
                                            <p>Any questions please send to email address: <strong>{4}</strong></p>";

        public const string SignatureFooter = @"<p>Thanks for using our service. If you have any problem with our service, feel free to share it with us by emailing the admin below: <a>tienanhcapuchino@gmail.com</a></p>
                                                <p>This email is sent automatically, <em><strong>please don&rsquo;t reply</strong></em>.</p>
                                                <p>&nbsp;</p>
                                                <p><em><strong>Best Regards,</strong></em></p>
                                                <p>&nbsp;<em><strong>Electronic Media Online Service</strong></em></p>
                                                <p><em>PRN231 - SUMMER 2023</em></p>
                                                <p><span style=""color: #3366ff;""><em>GROUP 10 - SE1614</em></span></p>
                                                <p><span style=""color: #3366ff;""><em>FPT University - Hoa Lac - Ha Noi - Viet Nam</em></span></p>
                                                <p><span style=""color: #3366ff;""><em>Contact: <a href=""mailto:tienanhcapuchino@gmail.com"">tienanhcapuchino@gmail.com</a></em></span></p>
                                                <p><strong><em>HAVE A GOOD EXPERIENCE!</em></strong></p>";
    }
    public class EmailTemplateSubjectConstant
    {
        public const string PostPublishedSubject = "Your post have been published";
        public const string CommentNotificationSubject = "Your post have a new comment";
        public const string PendingForApprovalPostSubject = "You have a post need to be approved";
        public const string AddNewUserSubject = "ADDED TO ELECTRONIC MEDIA ONLINE SERVICE";
        public const string DeletePostSubject = "Your post has been deleted";
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
        public const string DeletePostNotificationDes = "Notify to writer that your post is deleted!";

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
