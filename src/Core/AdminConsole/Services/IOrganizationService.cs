﻿// FIXME: Update this file to be null safe and then delete the line below
#nullable disable

using Bit.Core.AdminConsole.Entities;
using Bit.Core.AdminConsole.Models.Business;
using Bit.Core.Auth.Enums;
using Bit.Core.Billing.Models.Business;
using Bit.Core.Entities;
using Bit.Core.Enums;
using Bit.Core.Models.Business;
using Bit.Core.Models.Data;

namespace Bit.Core.Services;

public interface IOrganizationService
{
    Task CancelSubscriptionAsync(Guid organizationId, bool? endOfPeriod = null);
    Task ReinstateSubscriptionAsync(Guid organizationId);
    Task<string> AdjustStorageAsync(Guid organizationId, short storageAdjustmentGb);
    Task UpdateSubscription(Guid organizationId, int seatAdjustment, int? maxAutoscaleSeats);
    Task AutoAddSeatsAsync(Organization organization, int seatsToAdd);
    Task<string> AdjustSeatsAsync(Guid organizationId, int seatAdjustment);
    Task VerifyBankAsync(Guid organizationId, int amount1, int amount2);
    /// <summary>
    /// Create a new organization on a self-hosted instance
    /// </summary>
    Task<(Organization organization, OrganizationUser organizationUser)> SignUpAsync(OrganizationLicense license, User owner,
        string ownerKey, string collectionName, string publicKey, string privateKey);
    Task UpdateExpirationDateAsync(Guid organizationId, DateTime? expirationDate);
    Task UpdateAsync(Organization organization, bool updateBilling = false, EventType eventType = EventType.Organization_Updated);
    Task UpdateTwoFactorProviderAsync(Organization organization, TwoFactorProviderType type);
    Task DisableTwoFactorProviderAsync(Organization organization, TwoFactorProviderType type);
    Task<OrganizationUser> InviteUserAsync(Guid organizationId, Guid? invitingUserId, EventSystemUser? systemUser,
        OrganizationUserInvite invite, string externalId);
    Task<List<OrganizationUser>> InviteUsersAsync(Guid organizationId, Guid? invitingUserId, EventSystemUser? systemUser,
        IEnumerable<(OrganizationUserInvite invite, string externalId)> invites);
    Task<IEnumerable<Tuple<OrganizationUser, string>>> ResendInvitesAsync(Guid organizationId, Guid? invitingUserId, IEnumerable<Guid> organizationUsersId);
    Task ResendInviteAsync(Guid organizationId, Guid? invitingUserId, Guid organizationUserId, bool initOrganization = false);
    Task UpdateUserResetPasswordEnrollmentAsync(Guid organizationId, Guid userId, string resetPasswordKey, Guid? callingUserId);
    Task ImportAsync(Guid organizationId, IEnumerable<ImportedGroup> groups,
        IEnumerable<ImportedOrganizationUser> newUsers, IEnumerable<string> removeUserExternalIds,
        bool overwriteExisting, EventSystemUser eventSystemUser);
    Task DeleteSsoUserAsync(Guid userId, Guid? organizationId);
    Task RevokeUserAsync(OrganizationUser organizationUser, Guid? revokingUserId);
    Task RevokeUserAsync(OrganizationUser organizationUser, EventSystemUser systemUser);
    Task<List<Tuple<OrganizationUser, string>>> RevokeUsersAsync(Guid organizationId,
        IEnumerable<Guid> organizationUserIds, Guid? revokingUserId);
    Task ReplaceAndUpdateCacheAsync(Organization org, EventType? orgEvent = null);
    Task<(bool canScale, string failureReason)> CanScaleAsync(Organization organization, int seatsToAdd);

    void ValidatePasswordManagerPlan(Models.StaticStore.Plan plan, OrganizationUpgrade upgrade);
    void ValidateSecretsManagerPlan(Models.StaticStore.Plan plan, OrganizationUpgrade upgrade);
    Task ValidateOrganizationUserUpdatePermissions(Guid organizationId, OrganizationUserType newType,
        OrganizationUserType? oldType, Permissions permissions);
    Task ValidateOrganizationCustomPermissionsEnabledAsync(Guid organizationId, OrganizationUserType newType);
}
