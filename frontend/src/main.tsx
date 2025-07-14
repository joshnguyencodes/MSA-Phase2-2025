import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import HomePage from './pages/HomePage/HomePage.tsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Login from './pages/LoginPage/Login.tsx'
import Registration from './pages/LoginPage/Registration.tsx'



createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
       
        <Route path="/" element={<HomePage />} />
        <Route path='/login' element={<Login/>}/>
        <Route path = '/register' element={<Registration/>}/>

      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
