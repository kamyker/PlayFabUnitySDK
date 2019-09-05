#if !DISABLE_PLAYFABENTITY_API

using System;
using System.Collections.Generic;
using PlayFab.GroupsModels;
using PlayFab.Internal;
using System.Threading.Tasks;

namespace PlayFab
{
    /// <summary>
    /// The Groups API is designed for any permanent or semi-permanent collections of Entities (players, or non-players). If you
    /// want to make Guilds/Clans/Corporations/etc., then you should use groups. Groups can also be used to make chatrooms,
    /// parties, or any other persistent collection of entities.
    /// </summary>
    public static class PlayFabGroupsAPI
    {
        static PlayFabGroupsAPI() {}


        /// <summary>
        /// Verify entity login.
        /// </summary>
        public static bool IsEntityLoggedIn()
        {
            return PlayFabSettings.staticPlayer.IsEntityLoggedIn();
        }

        /// <summary>
        /// Clear the Client SessionToken which allows this Client to call API calls requiring login.
        /// A new/fresh login will be required after calling this.
        /// </summary>
        public static void ForgetAllCredentials()
        {
            PlayFabSettings.staticPlayer.ForgetAllCredentials();
        }

        private static PlayFabAuthenticationContext GetContext(SharedModels.PlayFabRequestCommon request) => (request == null ? null : request.AuthenticationContext) ?? PlayFabSettings.staticPlayer;

        /// <summary>
        /// Accepts an outstanding invitation to to join a group
        /// </summary>
        public static Task<EmptyResponse> AcceptGroupApplication(EntityKey Entity, EntityKey Group, 
            AcceptGroupApplicationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AcceptGroupApplicationRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/AcceptGroupApplication", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Accepts an invitation to join a group
        /// </summary>
        public static Task<EmptyResponse> AcceptGroupInvitation(EntityKey Group, EntityKey Entity = default, 
            AcceptGroupInvitationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AcceptGroupInvitationRequest();
            if(Group != default)
                request.Group = Group;
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/AcceptGroupInvitation", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds members to a group or role.
        /// </summary>
        public static Task<EmptyResponse> AddMembers(EntityKey Group, List<EntityKey> Members, string RoleId = default, 
            AddMembersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new AddMembersRequest();
            if(Group != default)
                request.Group = Group;
            if(Members != default)
                request.Members = Members;
            if(RoleId != default)
                request.RoleId = RoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/AddMembers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Applies to join a group
        /// </summary>
        public static Task<ApplyToGroupResponse> ApplyToGroup(EntityKey Group, bool? AutoAcceptOutstandingInvite = default, EntityKey Entity = default, 
            ApplyToGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ApplyToGroupRequest();
            if(Group != default)
                request.Group = Group;
            if(AutoAcceptOutstandingInvite != default)
                request.AutoAcceptOutstandingInvite = AutoAcceptOutstandingInvite;
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ApplyToGroupResponse>("/Group/ApplyToGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Blocks a list of entities from joining a group.
        /// </summary>
        public static Task<EmptyResponse> BlockEntity(EntityKey Entity, EntityKey Group, 
            BlockEntityRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new BlockEntityRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/BlockEntity", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Changes the role membership of a list of entities from one role to another.
        /// </summary>
        public static Task<EmptyResponse> ChangeMemberRole(EntityKey Group, List<EntityKey> Members, string OriginRoleId, string DestinationRoleId = default, 
            ChangeMemberRoleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ChangeMemberRoleRequest();
            if(Group != default)
                request.Group = Group;
            if(Members != default)
                request.Members = Members;
            if(OriginRoleId != default)
                request.OriginRoleId = OriginRoleId;
            if(DestinationRoleId != default)
                request.DestinationRoleId = DestinationRoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/ChangeMemberRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        public static Task<CreateGroupResponse> CreateGroup(string GroupName, EntityKey Entity = default, 
            CreateGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateGroupRequest();
            if(GroupName != default)
                request.GroupName = GroupName;
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateGroupResponse>("/Group/CreateGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new group role.
        /// </summary>
        public static Task<CreateGroupRoleResponse> CreateRole(EntityKey Group, string RoleId, string RoleName, 
            CreateGroupRoleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new CreateGroupRoleRequest();
            if(Group != default)
                request.Group = Group;
            if(RoleId != default)
                request.RoleId = RoleId;
            if(RoleName != default)
                request.RoleName = RoleName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<CreateGroupRoleResponse>("/Group/CreateRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a group and all roles, invitations, join requests, and blocks associated with it.
        /// </summary>
        public static Task<EmptyResponse> DeleteGroup(EntityKey Group, 
            DeleteGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteGroupRequest();
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/DeleteGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes an existing role in a group.
        /// </summary>
        public static Task<EmptyResponse> DeleteRole(EntityKey Group, string RoleId = default, 
            DeleteRoleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new DeleteRoleRequest();
            if(Group != default)
                request.Group = Group;
            if(RoleId != default)
                request.RoleId = RoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/DeleteRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets information about a group and its roles
        /// </summary>
        public static Task<GetGroupResponse> GetGroup(EntityKey Group = default, string GroupName = default, 
            GetGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new GetGroupRequest();
            if(Group != default)
                request.Group = Group;
            if(GroupName != default)
                request.GroupName = GroupName;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<GetGroupResponse>("/Group/GetGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Invites a player to join a group
        /// </summary>
        public static Task<InviteToGroupResponse> InviteToGroup(EntityKey Entity, EntityKey Group, bool? AutoAcceptOutstandingApplication = default, string RoleId = default, 
            InviteToGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new InviteToGroupRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;
            if(AutoAcceptOutstandingApplication != default)
                request.AutoAcceptOutstandingApplication = AutoAcceptOutstandingApplication;
            if(RoleId != default)
                request.RoleId = RoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<InviteToGroupResponse>("/Group/InviteToGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Checks to see if an entity is a member of a group or role within the group
        /// </summary>
        public static Task<IsMemberResponse> IsMember(EntityKey Entity, EntityKey Group, string RoleId = default, 
            IsMemberRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new IsMemberRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;
            if(RoleId != default)
                request.RoleId = RoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<IsMemberResponse>("/Group/IsMember", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all outstanding requests to join a group
        /// </summary>
        public static Task<ListGroupApplicationsResponse> ListGroupApplications(EntityKey Group, 
            ListGroupApplicationsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListGroupApplicationsRequest();
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListGroupApplicationsResponse>("/Group/ListGroupApplications", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all entities blocked from joining a group
        /// </summary>
        public static Task<ListGroupBlocksResponse> ListGroupBlocks(EntityKey Group, 
            ListGroupBlocksRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListGroupBlocksRequest();
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListGroupBlocksResponse>("/Group/ListGroupBlocks", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all outstanding invitations for a group
        /// </summary>
        public static Task<ListGroupInvitationsResponse> ListGroupInvitations(EntityKey Group, 
            ListGroupInvitationsRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListGroupInvitationsRequest();
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListGroupInvitationsResponse>("/Group/ListGroupInvitations", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all members for a group
        /// </summary>
        public static Task<ListGroupMembersResponse> ListGroupMembers(EntityKey Group, 
            ListGroupMembersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListGroupMembersRequest();
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListGroupMembersResponse>("/Group/ListGroupMembers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all groups and roles for an entity
        /// </summary>
        public static Task<ListMembershipResponse> ListMembership(EntityKey Entity = default, 
            ListMembershipRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListMembershipRequest();
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListMembershipResponse>("/Group/ListMembership", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all outstanding invitations and group applications for an entity
        /// </summary>
        public static Task<ListMembershipOpportunitiesResponse> ListMembershipOpportunities(EntityKey Entity = default, 
            ListMembershipOpportunitiesRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new ListMembershipOpportunitiesRequest();
            if(Entity != default)
                request.Entity = Entity;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<ListMembershipOpportunitiesResponse>("/Group/ListMembershipOpportunities", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes an application to join a group
        /// </summary>
        public static Task<EmptyResponse> RemoveGroupApplication(EntityKey Entity, EntityKey Group, 
            RemoveGroupApplicationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveGroupApplicationRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/RemoveGroupApplication", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes an invitation join a group
        /// </summary>
        public static Task<EmptyResponse> RemoveGroupInvitation(EntityKey Entity, EntityKey Group, 
            RemoveGroupInvitationRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveGroupInvitationRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/RemoveGroupInvitation", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes members from a group.
        /// </summary>
        public static Task<EmptyResponse> RemoveMembers(EntityKey Group, List<EntityKey> Members, string RoleId = default, 
            RemoveMembersRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new RemoveMembersRequest();
            if(Group != default)
                request.Group = Group;
            if(Members != default)
                request.Members = Members;
            if(RoleId != default)
                request.RoleId = RoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/RemoveMembers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unblocks a list of entities from joining a group
        /// </summary>
        public static Task<EmptyResponse> UnblockEntity(EntityKey Entity, EntityKey Group, 
            UnblockEntityRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UnblockEntityRequest();
            if(Entity != default)
                request.Entity = Entity;
            if(Group != default)
                request.Group = Group;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/UnblockEntity", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates non-membership data about a group.
        /// </summary>
        public static Task<UpdateGroupResponse> UpdateGroup(EntityKey Group, string AdminRoleId = default, int? ExpectedProfileVersion = default, string GroupName = default, string MemberRoleId = default, 
            UpdateGroupRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateGroupRequest();
            if(Group != default)
                request.Group = Group;
            if(AdminRoleId != default)
                request.AdminRoleId = AdminRoleId;
            if(ExpectedProfileVersion != default)
                request.ExpectedProfileVersion = ExpectedProfileVersion;
            if(GroupName != default)
                request.GroupName = GroupName;
            if(MemberRoleId != default)
                request.MemberRoleId = MemberRoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateGroupResponse>("/Group/UpdateGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates metadata about a role.
        /// </summary>
        public static Task<UpdateGroupRoleResponse> UpdateRole(EntityKey Group, string RoleName, int? ExpectedProfileVersion = default, string RoleId = default, 
            UpdateGroupRoleRequest request = default, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            if(request == null)
                request = new UpdateGroupRoleRequest();
            if(Group != default)
                request.Group = Group;
            if(RoleName != default)
                request.RoleName = RoleName;
            if(ExpectedProfileVersion != default)
                request.ExpectedProfileVersion = ExpectedProfileVersion;
            if(RoleId != default)
                request.RoleId = RoleId;

            var context = GetContext(request);

            return PlayFabHttp.MakeApiCallAsync<UpdateGroupRoleResponse>("/Group/UpdateRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
