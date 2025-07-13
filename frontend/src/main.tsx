import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import HomePage from './pages/HomePage.tsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
       
        <Route path="/" element={<HomePage />} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
