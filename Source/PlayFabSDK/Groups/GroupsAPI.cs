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
    public static class GroupsAPI
    {
        static GroupsAPI() {}


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

        private static PlayFabAuthenticationContext GetContext(PlayFabAuthenticationContext context) => context ?? PlayFabSettings.staticPlayer;

        /// <summary>
        /// Accepts an outstanding invitation to to join a group
        /// </summary>
        /// <param name="Entity">Optional. Type of the entity to accept as. If specified, must be the same entity as the claimant or an entity that is a child of the claimant entity. Defaults to the claimant entity. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<EmptyResponse> AcceptGroupApplication(EntityKey Entity, EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AcceptGroupApplicationRequest request = new AcceptGroupApplicationRequest()
            {
                Entity = Entity,
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/AcceptGroupApplication", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Accepts an invitation to join a group
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<EmptyResponse> AcceptGroupInvitation(EntityKey Group, EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AcceptGroupInvitationRequest request = new AcceptGroupInvitationRequest()
            {
                Group = Group,
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/AcceptGroupInvitation", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Adds members to a group or role.
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="Members">List of entities to add to the group. Only entities of type title_player_account and character may be added to groups. (Required)</param>
        /// <param name="RoleId">Optional: The ID of the existing role to add the entities to. If this is not specified, the default member role for the group will be used. Role IDs must be between 1 and 64 characters long. (Optional)</param>
        public static Task<EmptyResponse> AddMembers(EntityKey Group, List<EntityKey> Members, string RoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AddMembersRequest request = new AddMembersRequest()
            {
                Group = Group,
                Members = Members,
                RoleId = RoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/AddMembers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Applies to join a group
        /// </summary>
        /// <param name="AutoAcceptOutstandingInvite">Optional, default true. Automatically accept an outstanding invitation if one exists instead of creating an application (Optional)</param>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<ApplyToGroupResponse> ApplyToGroup(EntityKey Group, bool? AutoAcceptOutstandingInvite = default, EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ApplyToGroupRequest request = new ApplyToGroupRequest()
            {
                Group = Group,
                AutoAcceptOutstandingInvite = AutoAcceptOutstandingInvite,
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ApplyToGroupResponse>("/Group/ApplyToGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Blocks a list of entities from joining a group.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<EmptyResponse> BlockEntity(EntityKey Entity, EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            BlockEntityRequest request = new BlockEntityRequest()
            {
                Entity = Entity,
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/BlockEntity", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Changes the role membership of a list of entities from one role to another.
        /// </summary>
        /// <param name="DestinationRoleId">The ID of the role that the entities will become a member of. This must be an existing role. Role IDs must be between 1 and 64 characters long. (Optional)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="Members">List of entities to move between roles in the group. All entities in this list must be members of the group and origin role. (Required)</param>
        /// <param name="OriginRoleId">The ID of the role that the entities currently are a member of. Role IDs must be between 1 and 64 characters long. (Required)</param>
        public static Task<EmptyResponse> ChangeMemberRole(EntityKey Group, List<EntityKey> Members, string OriginRoleId, string DestinationRoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ChangeMemberRoleRequest request = new ChangeMemberRoleRequest()
            {
                Group = Group,
                Members = Members,
                OriginRoleId = OriginRoleId,
                DestinationRoleId = DestinationRoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/ChangeMemberRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        /// <param name="GroupName">The name of the group. This is unique at the title level by default. (Required)</param>
        public static Task<CreateGroupResponse> CreateGroup(string GroupName, EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateGroupRequest request = new CreateGroupRequest()
            {
                GroupName = GroupName,
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateGroupResponse>("/Group/CreateGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Creates a new group role.
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="RoleId">The ID of the role. This must be unique within the group and cannot be changed. Role IDs must be between 1 and 64 characters long. (Required)</param>
        /// <param name="RoleName">The name of the role. This must be unique within the group and can be changed later. Role names must be between 1 and 100 characters long (Required)</param>
        public static Task<CreateGroupRoleResponse> CreateRole(EntityKey Group, string RoleId, string RoleName, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            CreateGroupRoleRequest request = new CreateGroupRoleRequest()
            {
                Group = Group,
                RoleId = RoleId,
                RoleName = RoleName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<CreateGroupRoleResponse>("/Group/CreateRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes a group and all roles, invitations, join requests, and blocks associated with it.
        /// </summary>
        /// <param name="Group">ID of the group or role to remove (Required)</param>
        public static Task<EmptyResponse> DeleteGroup(EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteGroupRequest request = new DeleteGroupRequest()
            {
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/DeleteGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Deletes an existing role in a group.
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="RoleId">The ID of the role to delete. Role IDs must be between 1 and 64 characters long. (Optional)</param>
        public static Task<EmptyResponse> DeleteRole(EntityKey Group, string RoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            DeleteRoleRequest request = new DeleteRoleRequest()
            {
                Group = Group,
                RoleId = RoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/DeleteRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Gets information about a group and its roles
        /// </summary>
        /// <param name="Group">The identifier of the group (Optional)</param>
        /// <param name="GroupName">The full name of the group (Optional)</param>
        public static Task<GetGroupResponse> GetGroup(EntityKey Group = default, string GroupName = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            GetGroupRequest request = new GetGroupRequest()
            {
                Group = Group,
                GroupName = GroupName,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<GetGroupResponse>("/Group/GetGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Invites a player to join a group
        /// </summary>
        /// <param name="AutoAcceptOutstandingApplication">Optional, default true. Automatically accept an application if one exists instead of creating an invitation (Optional)</param>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="RoleId">Optional. ID of an existing a role in the group to assign the user to. The group's default member role is used if this is not specified. Role IDs must be between 1 and 64 characters long. (Optional)</param>
        public static Task<InviteToGroupResponse> InviteToGroup(EntityKey Entity, EntityKey Group, bool? AutoAcceptOutstandingApplication = default, string RoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            InviteToGroupRequest request = new InviteToGroupRequest()
            {
                Entity = Entity,
                Group = Group,
                AutoAcceptOutstandingApplication = AutoAcceptOutstandingApplication,
                RoleId = RoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<InviteToGroupResponse>("/Group/InviteToGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Checks to see if an entity is a member of a group or role within the group
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="RoleId">Optional: ID of the role to check membership of. Defaults to any role (that is, check to see if the entity is a member of the group in any capacity) if not specified. (Optional)</param>
        public static Task<IsMemberResponse> IsMember(EntityKey Entity, EntityKey Group, string RoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            IsMemberRequest request = new IsMemberRequest()
            {
                Entity = Entity,
                Group = Group,
                RoleId = RoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<IsMemberResponse>("/Group/IsMember", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all outstanding requests to join a group
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<ListGroupApplicationsResponse> ListGroupApplications(EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListGroupApplicationsRequest request = new ListGroupApplicationsRequest()
            {
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListGroupApplicationsResponse>("/Group/ListGroupApplications", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all entities blocked from joining a group
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<ListGroupBlocksResponse> ListGroupBlocks(EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListGroupBlocksRequest request = new ListGroupBlocksRequest()
            {
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListGroupBlocksResponse>("/Group/ListGroupBlocks", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all outstanding invitations for a group
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<ListGroupInvitationsResponse> ListGroupInvitations(EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListGroupInvitationsRequest request = new ListGroupInvitationsRequest()
            {
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListGroupInvitationsResponse>("/Group/ListGroupInvitations", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all members for a group
        /// </summary>
        /// <param name="Group">ID of the group to list the members and roles for (Required)</param>
        public static Task<ListGroupMembersResponse> ListGroupMembers(EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListGroupMembersRequest request = new ListGroupMembersRequest()
            {
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListGroupMembersResponse>("/Group/ListGroupMembers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all groups and roles for an entity
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        public static Task<ListMembershipResponse> ListMembership(EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListMembershipRequest request = new ListMembershipRequest()
            {
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListMembershipResponse>("/Group/ListMembership", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Lists all outstanding invitations and group applications for an entity
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Optional)</param>
        public static Task<ListMembershipOpportunitiesResponse> ListMembershipOpportunities(EntityKey Entity = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            ListMembershipOpportunitiesRequest request = new ListMembershipOpportunitiesRequest()
            {
                Entity = Entity,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<ListMembershipOpportunitiesResponse>("/Group/ListMembershipOpportunities", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes an application to join a group
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<EmptyResponse> RemoveGroupApplication(EntityKey Entity, EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveGroupApplicationRequest request = new RemoveGroupApplicationRequest()
            {
                Entity = Entity,
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/RemoveGroupApplication", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes an invitation join a group
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<EmptyResponse> RemoveGroupInvitation(EntityKey Entity, EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveGroupInvitationRequest request = new RemoveGroupInvitationRequest()
            {
                Entity = Entity,
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/RemoveGroupInvitation", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Removes members from a group.
        /// </summary>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="Members">List of entities to remove (Required)</param>
        /// <param name="RoleId">The ID of the role to remove the entities from. (Optional)</param>
        public static Task<EmptyResponse> RemoveMembers(EntityKey Group, List<EntityKey> Members, string RoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            RemoveMembersRequest request = new RemoveMembersRequest()
            {
                Group = Group,
                Members = Members,
                RoleId = RoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/RemoveMembers", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Unblocks a list of entities from joining a group
        /// </summary>
        /// <param name="Entity">The entity to perform this action on. (Required)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        public static Task<EmptyResponse> UnblockEntity(EntityKey Entity, EntityKey Group, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UnblockEntityRequest request = new UnblockEntityRequest()
            {
                Entity = Entity,
                Group = Group,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<EmptyResponse>("/Group/UnblockEntity", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates non-membership data about a group.
        /// </summary>
        /// <param name="AdminRoleId">Optional: the ID of an existing role to set as the new administrator role for the group (Optional)</param>
        /// <param name="ExpectedProfileVersion">Optional field used for concurrency control. By specifying the previously returned value of ProfileVersion from the GetGroup API, you can ensure that the group data update will only be performed if the group has not been updated by any other clients since the version you last loaded. (Optional)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="GroupName">Optional: the new name of the group (Optional)</param>
        /// <param name="MemberRoleId">Optional: the ID of an existing role to set as the new member role for the group (Optional)</param>
        public static Task<UpdateGroupResponse> UpdateGroup(EntityKey Group, string AdminRoleId = default, int? ExpectedProfileVersion = default, string GroupName = default, string MemberRoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateGroupRequest request = new UpdateGroupRequest()
            {
                Group = Group,
                AdminRoleId = AdminRoleId,
                ExpectedProfileVersion = ExpectedProfileVersion,
                GroupName = GroupName,
                MemberRoleId = MemberRoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateGroupResponse>("/Group/UpdateGroup", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }

        /// <summary>
        /// Updates metadata about a role.
        /// </summary>
        /// <param name="ExpectedProfileVersion">Optional field used for concurrency control. By specifying the previously returned value of ProfileVersion from the GetGroup API, you can ensure that the group data update will only be performed if the group has not been updated by any other clients since the version you last loaded. (Optional)</param>
        /// <param name="Group">The identifier of the group (Required)</param>
        /// <param name="RoleId">ID of the role to update. Role IDs must be between 1 and 64 characters long. (Optional)</param>
        /// <param name="RoleName">The new name of the role (Required)</param>
        public static Task<UpdateGroupRoleResponse> UpdateRole(EntityKey Group, string RoleName, int? ExpectedProfileVersion = default, string RoleId = default, 
            PlayFabAuthenticationContext customAuthContext = null, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            UpdateGroupRoleRequest request = new UpdateGroupRoleRequest()
            {
                Group = Group,
                RoleName = RoleName,
                ExpectedProfileVersion = ExpectedProfileVersion,
                RoleId = RoleId,
            };

            var context = GetContext(customAuthContext);

            return PlayFabHttp.MakeApiCallAsync<UpdateGroupRoleResponse>("/Group/UpdateRole", request,
				AuthType.EntityToken,
				customData, extraHeaders, context);
        }


    }
}

#endif
