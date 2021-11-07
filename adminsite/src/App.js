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
function App() {
  return (
    <Router>
      <Topbar />
      <div className="container">
        <Sidebar />
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route path="/users">
            <UserList />
          </Route>
          <Route path="/products">
            <ProductList />
          </Route>
          <Route path="/categories">
            <CategoryList />
          </Route>
          <Route path="/product/:productId/images">
            <ImageList />
          </Route>
          <Route exact path="/product/:productId">
            <Product />
          </Route>
          <Route path="/category/:categoryId">
            <Category />
          </Route>
          <Route exact path="/product/:productId/image/:imageId">
            <Image />
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
