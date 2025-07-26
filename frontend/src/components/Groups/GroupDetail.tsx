import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './GroupDetail.css';

interface User {
  id: number;
  username: string;
}

interface GroupMember {
  id: number;
  user: User;
  joinedAt: string;
  role: string;
}

interface Order {
  id: number;
  name: string;
  targetAmount: number;
  createdAt: string;
  status: string;
}

interface GroupDetail {
  id: number;
  name: string;
  description?: string;
  theme: string;
  createdAt: string;
  creator: User;
  members: GroupMember[];
  orders: Order[];
  isUserMember: boolean;
  userRole?: string;
}

const GroupDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [group, setGroup] = useState<GroupDetail | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [actionLoading, setActionLoading] = useState(false);

  useEffect(() => {
    if (id) {
      fetchGroupDetail();
    }
  }, [id]);

  const fetchGroupDetail = async () => {
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`/api/group/${id}`, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        const data = await response.json();
        setGroup(data);
      } else if (response.status === 404) {
        setError('Group not found');
      } else {
        throw new Error('Failed to fetch group details');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  const handleJoinGroup = async () => {
    if (!group) return;
    
    setActionLoading(true);
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`/api/group/${group.id}/join`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        await fetchGroupDetail(); // Refresh the group data
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to join group');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setActionLoading(false);
    }
  };

  const handleLeaveGroup = async () => {
    if (!group) return;
    
    if (!confirm('Are you sure you want to leave this group?')) return;
    
    setActionLoading(true);
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`/api/group/${group.id}/leave`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        navigate('/groups');
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to leave group');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setActionLoading(false);
    }
  };

  const handleDeleteGroup = async () => {
    if (!group) return;
    
    if (!confirm('Are you sure you want to delete this group? This action cannot be undone.')) return;
    
    setActionLoading(true);
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`/api/group/${group.id}`, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        navigate('/groups');
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to delete group');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setActionLoading(false);
    }
  };

  if (loading) return <div className="loading">Loading group details...</div>;
  if (error) return <div className="error">Error: {error}</div>;
  if (!group) return <div className="error">Group not found</div>;

  const isCreator = group.userRole === 'Creator';

  return (
    <div className="group-detail-container">
      <div className="group-detail-header">
        <button className="back-btn" onClick={() => navigate('/groups')}>
          ← Back to Groups
        </button>
        <div className="group-actions">
          {group.isUserMember ? (
            <>
              {!isCreator && (
                <button
                  className="leave-btn"
                  onClick={handleLeaveGroup}
                  disabled={actionLoading}
                >
                  {actionLoading ? 'Leaving...' : 'Leave Group'}
                </button>
              )}
              {isCreator && (
                <button
                  className="delete-btn"
                  onClick={handleDeleteGroup}
                  disabled={actionLoading}
                >
                  {actionLoading ? 'Deleting...' : 'Delete Group'}
                </button>
              )}
            </>
          ) : (
            <button
              className="join-btn"
              onClick={handleJoinGroup}
              disabled={actionLoading}
            >
              {actionLoading ? 'Joining...' : 'Join Group'}
            </button>
          )}
        </div>
      </div>

      <div className="group-info-card">
        <div className="group-theme-badge">{group.theme}</div>
        <h1>{group.name}</h1>
        {group.description && (
          <p className="group-description">{group.description}</p>
        )}
        <div className="group-meta">
          <span>Created by: <strong>{group.creator.username}</strong></span>
          <span>Created: {new Date(group.createdAt).toLocaleDateString()}</span>
          <span>Members: <strong>{group.members.length}</strong></span>
        </div>
      </div>

      <div className="group-content">
        <div className="group-section">
          <h2>Members ({group.members.length})</h2>
          <div className="members-list">
            {group.members.map((member) => (
              <div key={member.id} className="member-card">
                <div className="member-info">
                  <span className="member-name">{member.user.username}</span>
                  <span className={`member-role ${member.role.toLowerCase()}`}>
                    {member.role}
                  </span>
                </div>
                <span className="member-joined">
                  Joined: {new Date(member.joinedAt).toLocaleDateString()}
                </span>
              </div>
            ))}
          </div>
        </div>

        <div className="group-section">
          <div className="section-header">
            <h2>Orders ({group.orders.length})</h2>
            {group.isUserMember && (
              <button className="create-order-btn">
                Create Order
              </button>
            )}
          </div>
          {group.orders.length === 0 ? (
            <div className="no-orders">
              {group.isUserMember 
                ? "No orders yet. Be the first to create one!" 
                : "No orders available in this group."}
            </div>
          ) : (
            <div className="orders-list">
              {group.orders.map((order) => (
                <div key={order.id} className="order-card">
                  <h3>{order.name}</h3>
                  <div className="order-info">
                    <span>Target: ${order.targetAmount}</span>
                    <span className={`order-status ${order.status.toLowerCase()}`}>
                      {order.status}
                    </span>
                    <span>Created: {new Date(order.createdAt).toLocaleDateString()}</span>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default GroupDetail;
