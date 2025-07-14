import React from 'react';
import { Link } from 'react-router-dom';
import './HomePage.css';


const HomePage = () => {
  return (
    <div className="home-page">
      <h1>Welcome to BulkBuy</h1>
      <div className="buttons">
        <Link to="/login" className="button">Login</Link>
        <Link to="/register" className="button">Register</Link>
        <Link to="/create-group" className="button">Create Group</Link>
        <Link to="/groups" className="button">Groups</Link>
        <Link to="/orders" className="button">Orders</Link>
        <Link to="/dashboard" className="button">Dashboard</Link>
      </div>
      <div className="group-list">
        <h2>Available Buying Groups</h2>
      </div>
    </div>
  );
};

export default HomePage;