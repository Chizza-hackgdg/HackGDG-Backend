using AutoMapper;
using Core.Utilities.Results;
using Dto.ForumComments;
using Dto.ForumPosts;
using Entity;
using Microsoft.AspNetCore.Identity;
using Service.Abstracts;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.UserProfessions;

namespace Service.Concretes
{
    public class ForumService : IForumService
    {
        public ITBaseService<ForumPost, Guid> ForumPostCurrent { get; }
        public ITBaseService<ForumComment, Guid> ForumCommentCurrent { get; }
        public ITBaseService<ForumPostMatchSubmitUser, Guid> ForumPostMatchCurrent { get; }
        public ITBaseService<UserProfession, Guid> UserProfessionCurrent { get; }

        ITBaseService<ForumPost, Guid> IDbOperationEvent<ForumPost, Guid>.Current => ForumPostCurrent;
        ITBaseService<ForumComment, Guid> IDbOperationEvent<ForumComment, Guid>.Current => ForumCommentCurrent;
        ITBaseService<ForumPostMatchSubmitUser, Guid> IDbOperationEvent<ForumPostMatchSubmitUser, Guid>.Current => ForumPostMatchCurrent;
        ITBaseService<UserProfession, Guid> IDbOperationEvent<UserProfession, Guid>.Current => UserProfessionCurrent;


        public ITBaseService<ForumPostMatchSubmitUser, Guid> Current => throw new NotImplementedException();


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserProfessionService _userProfessionService;

        private readonly IMapper _mapper;
        public ForumService(ITBaseService<ForumPost, Guid> forumPostService, ITBaseService<ForumComment, Guid> forumCommentService, IMapper mapper, ITBaseService<ForumPostMatchSubmitUser, Guid> forumPostMatchCurrent, UserManager<ApplicationUser> userManager, IUserProfessionService userProfessionService, ITBaseService<UserProfession, Guid> userProfessionCurrent)
        {
            ForumPostCurrent = forumPostService;
            ForumCommentCurrent = forumCommentService;
            _mapper = mapper;
            ForumPostMatchCurrent = forumPostMatchCurrent;
            _userManager = userManager;
            _userProfessionService = userProfessionService;
            UserProfessionCurrent = userProfessionCurrent;
        }

        public async Task<IAsyncResult> CreateCommentAsync(CreateForumCommentDto commentDto)
        {
            var commentMap = _mapper.Map<ForumComment>(commentDto);
            commentMap.CreatedDate = DateTime.UtcNow;
            var result = await ForumCommentCurrent.AddAsync(commentMap);
            return new AsyncSuccessResult();

        }

        public async Task<IAsyncResult> CreatePostAsync(CreateForumPostDto postDto)
        {
            var postMap = _mapper.Map<ForumPost>(postDto);
            postMap.CreatedDate = DateTime.UtcNow;
            var result = await ForumPostCurrent.AddAsync(postMap);
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncDataResult<List<GetForumCommentDto>>> GetAllCommentsAsync()
        {
            var result = await ForumCommentCurrent.GetAllListAsync();
            var commentDtos = _mapper.Map<List<GetForumCommentDto>>(result);
            return new AsyncSuccessDataResult<List<GetForumCommentDto>>(Task.FromResult(commentDtos));
        }

        public async Task<IAsyncDataResult<List<GetForumPostDto>>> GetAllPostsAsync()
        {
            var result = await ForumPostCurrent.GetAllListAsync();
            var postDtos = _mapper.Map<List<GetForumPostDto>>(result);
            return new AsyncSuccessDataResult<List<GetForumPostDto>>(Task.FromResult(postDtos));
        }

        public async Task<IAsyncDataResult<GetForumCommentDto>> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await ForumCommentCurrent.FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null)
            {
                return new AsyncErrorDataResult<GetForumCommentDto>(Task.FromResult("Comment not found"));
            }
            var commentDto = _mapper.Map<GetForumCommentDto>(comment);
            return new AsyncSuccessDataResult<GetForumCommentDto>(Task.FromResult(commentDto));
        }

        public async Task<IAsyncDataResult<GetForumPostDto>> GetPostByIdAsync(Guid postId)
        {
            var post = await ForumPostCurrent.FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return new AsyncErrorDataResult<GetForumPostDto>(Task.FromResult("Post not found"));
            }
            var postDto = _mapper.Map<GetForumPostDto>(post);
            return new AsyncSuccessDataResult<GetForumPostDto>(Task.FromResult(postDto));
        }

        public async Task<IAsyncResult> SoftDeleteCommentAsync(Guid commentId)
        {
            var comment = await ForumCommentCurrent.FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null)
            {
                return new AsyncErrorResult(Task.FromResult("Comment not found"));
            }
            comment.IsDeleted = true;
            comment.DeletedDate = DateTime.UtcNow;
            var result = await ForumCommentCurrent.UpdateAsync(comment);
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncResult> SoftDeletePostAsync(Guid postId)
        {
            var post = await ForumPostCurrent.FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return new AsyncErrorResult(Task.FromResult("Post not found"));
            }
            post.IsDeleted = true;
            post.DeletedDate = DateTime.UtcNow;
            var result = await ForumPostCurrent.UpdateAsync(post);
            return new AsyncSuccessResult();

        }

        public async Task<IAsyncResult> UpdateCommentAsync(UpdateForumCommentDto commentDto)
        {
            var commentMap = _mapper.Map<ForumComment>(commentDto);
            commentMap.UpdatedDate = DateTime.UtcNow;
            var result = await ForumCommentCurrent.UpdateAsync(commentMap);
            return new AsyncSuccessResult();
        }

        public async Task<Core.Utilities.Results.IAsyncResult> UpdatePostAsync(UpdateForumPostDto postDto)
        {
            var postMap = _mapper.Map<ForumPost>(postDto);
            postMap.UpdatedDate = DateTime.UtcNow;
            var result = await ForumPostCurrent.UpdateAsync(postMap);
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncDataResult<List<GetForumCommentDto>>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await ForumCommentCurrent.GetAllListAsync(x => x.ForumPostId == postId);
            var commentDtos = _mapper.Map<List<GetForumCommentDto>>(comments).ToList();
            return new AsyncSuccessDataResult<List<GetForumCommentDto>>(Task.FromResult(commentDtos));

        }

        public async Task<IAsyncDataResult<List<GetForumCommentDto>>> GetChildCommentsByMainId(Guid commentId)
        {
            var childComments = await ForumCommentCurrent.GetAllListAsync(x => x.MainComment == commentId);
            var commentMap = _mapper.Map<List<GetForumCommentDto>>(childComments);
            return new AsyncSuccessDataResult<List<GetForumCommentDto>>(Task.FromResult(commentMap));
        }

        public async Task<Core.Utilities.Results.IAsyncResult> SubmitForForumPostMatchAsync(SubmitForumMatchDto submitDto)
        {
            var forumPost = await ForumPostCurrent.FirstOrDefaultAsync(x => x.Id == submitDto.ForumPostId);
            if (forumPost == null)
            {
                return new AsyncErrorResult(Task.FromResult("Forum post not found."));
            }
            if (forumPost.PostType != Core.Enum.Enum_ForumPostType.Profession)
            {
                return new AsyncErrorResult(Task.FromResult("You can not make submission to this post."));
            }

            var existingSubmission = await ForumPostMatchCurrent.FirstOrDefaultAsync(x =>
                x.UserId == submitDto.UserId);

            if (existingSubmission != null)
            {
                return new AsyncErrorResult(Task.FromResult("You have already submitted for this forum post."));
            }
            var submitMap = _mapper.Map<ForumPostMatchSubmitUser>(submitDto);
            submitMap.CreatedDate = DateTime.UtcNow;
            await ForumPostMatchCurrent.AddAsync(submitMap);
            return new AsyncSuccessResult(Task.FromResult("Match submission successful."));


        }

        public async Task<Core.Utilities.Results.IAsyncResult> MatchAllUsersForAllForumPostsAsync()
        {

            var forumPosts = await ForumPostCurrent.GetAllListAsync();

            foreach (var forumPost in forumPosts)
            {
                for (int skillLevel = 1; skillLevel <= 4; skillLevel++)
                {
                    var userList = await ForumPostMatchCurrent.GetAllListAsync();
                    foreach (var user in userList)
                    {
                        if (_userProfessionService.GetUserProfessionsByUserIdAsync(user.Id).Result.Data != null)
                        {
                            continue;
                        }
                        var userAdd = _mapper.Map<UserProfession>(user);
                        userAdd.ForumPostId = forumPost.Id;
                        userAdd.ProfessionName = forumPost.Title;
                        userAdd.ProfessionDescription = forumPost.Description;
                        userAdd.IsActive = true;
                        await _userProfessionService.CreateUserProfessionAsync(_mapper.Map<CreateUserProfessionDto>(userAdd));
                        await DeactivateOtherUserProfessions(userAdd.UserId, userAdd.Id);

                    }
                    var users = await ForumPostMatchCurrent.GetAllListAsync(x => x.ForumPostId == forumPost.Id);
                    var random = new Random();
                    users = users.OrderBy(x => random.Next()).ToList();

                    for (int i = 0; i < users.Count; i++)
                    {

                        try
                        {
                            var user1 = userList[i];
                            var user2 = userList[i + 1];

                            var applicationUser1 = await _userManager.FindByIdAsync(user1.UserId.ToString());
                            var applicationUser2 = await _userManager.FindByIdAsync(user2.UserId.ToString());

                            if (applicationUser1 != null && applicationUser2 != null)
                            {
                                applicationUser1.MatchedUserId = applicationUser2.Id;
                                applicationUser2.MatchedUserId = applicationUser1.Id;

                                await _userManager.UpdateAsync(applicationUser1);
                                await _userManager.UpdateAsync(applicationUser2);
                            }
                            else
                            {
                                // Leave the last user unmatched
                                var unmatchedUser = users[i];
                                var applicationUser = await _userManager.FindByIdAsync(unmatchedUser.UserId.ToString());

                                if (applicationUser != null)
                                {
                                    applicationUser.MatchedUserId = null; // Explicitly set as unmatched
                                    await _userManager.UpdateAsync(applicationUser);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }

                    // Exit all loops
                    return new AsyncSuccessResult(Task.FromResult("All users matched successfully."));
                }
            }

            return new AsyncSuccessResult(Task.FromResult("All users matched successfully."));
        }
        private async Task DeactivateOtherUserProfessions(Guid userId, Guid currentProfessionId)
        {
            // Get all UserProfession entries for the user except the current one
            var userProfessions = await UserProfessionCurrent.GetAllListAsync(x => x.UserId == userId && x.Id != currentProfessionId);

            foreach (var profession in userProfessions)
            {
                profession.IsActive = false;
                await UserProfessionCurrent.UpdateAsync(profession);
            }
        }

    }
}
