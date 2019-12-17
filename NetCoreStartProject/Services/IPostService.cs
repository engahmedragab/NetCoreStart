using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();

        Task<bool> CreatePostAsync(Post post);

        Task<Post> GetPostByIdAsync(Guid postId);

        Task<bool> UpdatePostAsync(Post postToUpdate);

        Task<bool> DeletePostAsync(Guid postId);
        
        Task<bool> UserOwnsPostAsync(Guid postId, string userId);
    }
}