import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './CreateGroup.css';

interface CreateGroupFormData {
  name: string;
  description: string;
  theme: string;
}

const CreateGroup: React.FC = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<CreateGroupFormData>({
    name: '',
    description: '',
    theme: '',
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const themes = [
    'Electronics & Technology',
    'Food & Beverages',
    'Health & Wellness',
    'Home & Garden',
    'Fashion & Clothing',
    'Books & Education',
    'Sports & Fitness',
    'Travel & Adventure',
    'Arts & Crafts',
    'Business & Professional',
    'Other'
  ];

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const token = localStorage.getItem('token');
      const response = await fetch('/api/group', {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          name: formData.name,
          description: formData.description || null,
          theme: formData.theme
        }),
      });

      if (response.ok) {
        const createdGroup = await response.json();
        navigate(`/groups/${createdGroup.id}`);
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to create group');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="create-group-container">
      <div className="create-group-header">
        <h1>Create New Buying Group</h1>
        <button className="back-btn" onClick={() => navigate('/groups')}>
          ← Back to Groups
        </button>
      </div>

      <form onSubmit={handleSubmit} className="create-group-form">
        {error && <div className="error-message">{error}</div>}
        
        <div className="form-group">
          <label htmlFor="name">Group Name *</label>
          <input
            type="text"
            id="name"
            name="name"
            value={formData.name}
            onChange={handleInputChange}
            required
            maxLength={100}
            placeholder="Enter a descriptive name for your buying group"
          />
        </div>

        <div className="form-group">
          <label htmlFor="theme">Theme *</label>
          <select
            id="theme"
            name="theme"
            value={formData.theme}
            onChange={handleInputChange}
            required
          >
            <option value="">Select a theme</option>
            {themes.map(theme => (
              <option key={theme} value={theme}>{theme}</option>
            ))}
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="description">Description</label>
          <textarea
            id="description"
            name="description"
            value={formData.description}
            onChange={handleInputChange}
            maxLength={500}
            rows={4}
            placeholder="Describe the purpose and focus of your buying group (optional)"
          />
          <small className="char-count">
            {formData.description.length}/500 characters
          </small>
        </div>

        <div className="form-actions">
          <button 
            type="button" 
            className="cancel-btn"
            onClick={() => navigate('/groups')}
          >
            Cancel
          </button>
          <button 
            type="submit" 
            className="submit-btn"
            disabled={loading}
          >
            {loading ? 'Creating...' : 'Create Group'}
          </button>
        </div>
      </form>
    </div>
  );
};

export default CreateGroup;
