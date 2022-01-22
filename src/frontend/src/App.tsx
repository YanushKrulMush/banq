import React from "react";
import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";
import PrivateRoute from "./utility/PrivateRoute";
import Register from "./pages/Register";
import SideMenu from "./components/SideMenu";
import Makler from "./pages/Makler";

function App() {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/login">
          <Login />
        </Route>
        <Route exact path="/register">
          <Register />
        </Route>
        <PrivateRoute exact path="/">
          <Home />
        </PrivateRoute>
        <PrivateRoute exact path="/makler">
          <Makler />
        </PrivateRoute>
      </Switch>
    </BrowserRouter>
  );
}

export default App;
