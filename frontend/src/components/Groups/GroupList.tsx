import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './GroupList.css';

interface Group {
  id: number;
  name: string;
  description?: string;
  theme: string;
  createdAt: string;
  creator: {
    id: number;
    username: string;
  };
  memberCount: number;
  isUserMember: boolean;
}

const GroupList: React.FC = () => {
  const [groups, setGroups] = useState<Group[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showMyGroups, setShowMyGroups] = useState(false);

  useEffect(() => {
    fetchGroups();
  }, [showMyGroups]);

  const fetchGroups = async () => {
    try {
      const token = localStorage.getItem('token');
      const endpoint = showMyGroups ? '/api/group/my-groups' : '/api/group';
      
      const response = await fetch(endpoint, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error('Failed to fetch groups');
      }

      const data = await response.json();
      setGroups(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  const handleJoinGroup = async (groupId: number) => {
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`/api/group/${groupId}/join`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        // Refresh the groups list
        fetchGroups();
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to join group');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    }
  };

  const handleLeaveGroup = async (groupId: number) => {
    try {
      const token = localStorage.getItem('token');
      const response = await fetch(`/api/group/${groupId}/leave`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        // Refresh the groups list
        fetchGroups();
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to leave group');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    }
  };

  if (loading) return <div className="loading">Loading groups...</div>;
  if (error) return <div className="error">Error: {error}</div>;

  return (
    <div className="group-list-container">
      <div className="group-list-header">
        <h1>Buying Groups</h1>
        <div className="group-actions">
          <button
            className={`toggle-btn ${!showMyGroups ? 'active' : ''}`}
            onClick={() => setShowMyGroups(false)}
          >
            All Groups
          </button>
          <button
            className={`toggle-btn ${showMyGroups ? 'active' : ''}`}
            onClick={() => setShowMyGroups(true)}
          >
            My Groups
          </button>
          <Link to="/groups/create" className="create-btn">
            Create Group
          </Link>
        </div>
      </div>

      {groups.length === 0 ? (
        <div className="no-groups">
          {showMyGroups ? "You haven't joined any groups yet." : "No groups available."}
        </div>
      ) : (
        <div className="groups-grid">
          {groups.map((group) => (
            <div key={group.id} className="group-card">
              <div className="group-theme">{group.theme}</div>
              <h3>{group.name}</h3>
              {group.description && <p className="group-description">{group.description}</p>}
              <div className="group-info">
                <span className="creator">Created by: {group.creator.username}</span>
                <span className="member-count">{group.memberCount} members</span>
                <span className="created-date">
                  Created: {new Date(group.createdAt).toLocaleDateString()}
                </span>
              </div>
              <div className="group-actions">
                <Link to={`/groups/${group.id}`} className="view-btn">
                  View Details
                </Link>
                {group.isUserMember ? (
                  <button
                    className="leave-btn"
                    onClick={() => handleLeaveGroup(group.id)}
                  >
                    Leave Group
                  </button>
                ) : (
                  <button
                    className="join-btn"
                    onClick={() => handleJoinGroup(group.id)}
                  >
                    Join Group
                  </button>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default GroupList;
