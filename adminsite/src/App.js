import Sidebar from "./components/sidebar/Sidebar";
import Topbar from "./components/topbar/Topbar";
import "./App.css";
import Home from "./pages/home/Home";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import UserList from "./pages/userList/UserList";
import ProductList from "./pages/productList/ProductList";
import Product from "./pages/product/Product";
import CategoryList from "./pages/categoryList/CategoryList";
import Category from "./pages/category/Category";
import ImageList from "./pages/imageList/ImageList";
import Image from "./pages/image/Image";
import Login from "./components/login/Login";
import { PrivateRoute } from "./route";
function App() {
  return (
    <Router>
      <Topbar />
      <div className="container">
        <Sidebar />
        <Switch>
          <Route exact path="/login">
            <Login />
          </Route>
          <PrivateRoute exact path="/">
            <Home />
          </PrivateRoute>
          <PrivateRoute exact path="/home">
            <Home />
          </PrivateRoute>
          <PrivateRoute exact path="/users">
            <UserList />
          </PrivateRoute>
          <PrivateRoute exact path="/products">
            <ProductList />
          </PrivateRoute>
          <PrivateRoute exact path="/categories">
            <CategoryList />
          </PrivateRoute>
          <PrivateRoute exact path="/product/:productId/images">
            <ImageList />
          </PrivateRoute>
          <PrivateRoute exact path="/product/:productId">
            <Product />
          </PrivateRoute>
          <PrivateRoute exact path="/category/:categoryId">
            <Category />
          </PrivateRoute>
          <PrivateRoute exact path="/product/:productId/image/:imageId">
            <Image />
          </PrivateRoute>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
