import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import HomePage from './pages/HomePage/HomePage.tsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Login from './pages/LoginPage/Login.tsx'
import Registration from './pages/LoginPage/Registration.tsx'
import GroupsPage from './pages/GroupsPage/GroupsPage.tsx'
import CreateGroupPage from './pages/GroupsPage/CreateGroupPage.tsx'
import GroupDetailPage from './pages/GroupsPage/GroupDetailPage.tsx'



createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
       
        <Route path="/" element={<HomePage />} />
        <Route path='/login' element={<Login/>}/>
        <Route path = '/register' element={<Registration/>}/>
        <Route path="/groups" element={<GroupsPage />} />
        <Route path="/groups/create" element={<CreateGroupPage />} />
        <Route path="/groups/:id" element={<GroupDetailPage />} />

      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
