![CINN Logo](https://github.com/csinn/CSInn/tree/master/Docs/Image Assets/CINNLogo.png) 

<section style="float: left; width: 250px; height: 500px; background-color: antiquewhite">

## Navigation

*   [Project Source](https://github.com/csinn/CSInn)
*   [Project Workflow](https://github.com/csinn/CSInn/projects/1)

</section>

<article class="CINNArticle">

## Project Requirements

### Guest Specifications

#### What should be included:

1.  **Login-** logging in would be nothing else than getting info about user account (if user has a discord account and if he or she is a member of C# Inn). Successful login redirects user to home page. Unsuccessful login will suggest creating a discord account and / or becoming a member of C# Inn.

3.  **View lesson material-** consists of a search bar for filtering and ordering lesson by name, category, rating, author, popularity. The content for lesson is made of a combination of: slides, video, description, raw recording, author, category tag, lesson length (time you will need to learn), views and rating (maybe).

5.  **View code reviews (View PR, video) -** code review is at least 2 elements: link to repository and comments about it. Ideally, it would be nice to render the PR request which was done during code review and display changes like a different tool.

7.  **View Community events calendar-** community events include: lesson, code review, challenges (range of dates), other events (game night, meetup, etc)

9.  **View Feedback page-** all the good words about us community should be put publicly in the feedback. Author should be seen (?). Feedback page should have a playful look and should not be just a list of responses. It should support different rotations, sizes, fonts to illustrate the variety of members that we have.

11.  **View Projects page-** should contain link to repository, author, description and comments (?)

#### What could be included:

1.  **Rate content-** from the search page (or from specific content view page) there should be a button to like content. Content likes count should be displayed with it.

3.  **Comment on project-** by opening a specific project, there could be a forum like structure, where comments could be posted. It could be a comment about the project, request for join, insight, etc.

5.  **Apply to be admin-** application to be admin works the same as for other things, but such member needs to qualify before this option is visible. They need to be a member for at least half a quarter of year, have talked at least weekly. On top of that, it needs to get approved by not one, but at least 3 other admins. Approval status will be visible in either case.

7.  **See other members online-** just like discord, it might be a good idea to see other members online: their avatars, names, logging in time, status. A minimalistic version of this is to see a summary of logged, idle and total members.

#### What won't be included:

1.  **Manage their discord account form our website-** our website is not supposed to replace discord. Our goal is to supplement discord with this website.

3.  **Offer paid work-** offering money for our services is against our way of thinking. Two 3rd parties can get in touch, however, we will not be responsible for the outcome and we will not support either of the two. We want to be accessible, we don’t seek for profit.

* * *

### Member Specification

#### What should be included:

1.  **View Partners page-** page with related entities such as people (youtubers, twitcher.. ). Should contain link to partner, their name, description, type of contact (Discord, YouTube, twitch..)

3.  **Request code review-** code review request should be done by flagging a range of datetime that suits you. Along with the code review user should provide link to public repository, short description about the project. Sent request has a pending status by default.

5.  **Request lesson-** request will contain a name, short description, discord id of the requestor. A new request will have a default status pending, which can later be accepted or declined.

7.  **Apply to become a mentor-** members can become mentors. By becoming a mentor you will have to provide your discord id, short motivational description, topics you are interested in mentoring. It will come with the status pending. You will need to have a talk with an admin to get your status approved and double checked in terms of your skill

9.  **Add project to projects board-** other than the things described before, project submission will have a status: pending, declined, accepted.

11.  **Subscribe to community events-** community events: lesson, code review, game night, meeting, challenge (date range) should contain a start time and end time. Ideally, they should be displayed in local time of the viewer. Subscribing to an event should be done from calendar view, just by clicking a day and selecting event (if there are more than one event)

13.  **Be notified about subscribed events-** a day and an hour before and at the start of an event you will get a notification (to email, probably) that event is happening in that much time.

15.  **Post on feedback page-** only verified members can post on the feedback page. After feedback has been posted, it will have the statuses: pending, closed, accepted. Accepted feedback is automatically composed on the feedback page.

#### What would be nice to include:

1.  **Flag self as ready to chat-** for the chatting requested by others, mentors can flag themselves as ready to answer the question or join a conversation.

3.  **Join a requested chat-** mentors who flagged themselves as ready to converse should form a matchmaking relation with those who requested to chat. Chat should be private, secure and there will not be any history of it afterwards.

5.  **Content review (to accept or decline posting review, add suggestions)-** as mentioned, content posted by mentors will have to be approved by at least two other mentors. Pending content will be a separate page.

* * *

### Administrator Specification

#### What should be included:

1.  **Moderate content:** delete, archive, edit- administrator can do a full CRUD for content.

3.  **Manage users (promote to mentor, ban, kick) -** admins can promote members to mentors and mentors to admins. For a member to become a mentor, they need to be approved by 2 other mentors or admins. For a mentor or member to be an admin, they need to be approved by 3 others admins.

#### What could be included:

1.  **Accept other admin applications-** within pending requests page admins can decline or accept a submission for admin position.

3.  **View website analytics, member Flow, other stats-** maybe a direct reference to google analytics, maybe our own way of integrating it. In any case, a page with stats such as retention rate, member flow, graphic of demographics, messaging, content posting, request stats, event stats, etc.

5.  **Answer question sent by guest or a member (from website)-** there could be a menu section for requests, where all the pending status would be visible. In this case that would be just a pending answer.

#### What would be nice to be included:

1.  **Sync discord content with website-** syncing content is a heavy task, which is risky to automate. Thus it should be done at will, rather than periodically. There would be a special page for managing content sync, with options to sync roles, channels, users.

</article>
