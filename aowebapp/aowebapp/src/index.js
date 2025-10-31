import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './routes/Home';
import Contact from './routes/Contact';
import CardListSearch from './components/CardList';
import CardDetail from './components/CardDetail';
import Graph from './routes/Graph';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <BrowserRouter>
            <App />
            <Routes>
                <Route path="/" element={<App />} />
                <Route path="Home" element={<Home />} ></Route>
                <Route path="Contact" element={<Contact />}></Route>
                <Route path="Graph" element={<Graph />}></Route>
                <Route path="Products" element={<CardListSearch />}></Route>
                <Route path="" element={<Home />}></Route>
                <Route path="*" element={<Home />}></Route>
                <Route path="Products/:itemId" element={<CardDetail />} />
            </Routes>
        </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
