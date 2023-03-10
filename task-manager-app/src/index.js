import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import Home from './Home';
import AddTask from "./Models/AddTask";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
const router = createBrowserRouter([
  {
    path: "/",
    element:
      <Home />

  },
  {
    path: "/addTask",
    element: <AddTask />,
  },
]);
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
