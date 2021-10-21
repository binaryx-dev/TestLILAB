import React, { useEffect, useState, useCallback } from 'react';
import _ from 'lodash';
import Container from '@material-ui/core/Container';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import Axios from 'axios';
import { Grid } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import Fab from '@material-ui/core/Fab';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import MenuItem from '@material-ui/core/MenuItem';
import { Alert, AlertTitle } from '@material-ui/lab';

import IconButton from '@material-ui/core/IconButton';
import AddIcon from '@material-ui/icons/Add';
import RemoveIcon from '@material-ui/icons/Remove';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';

const useStyles = makeStyles({
    root: {
        maxWidth: 345,
    },
    media: {
        height: 140,
    },
    fab: {
        position: "fixed",
        bottom: 30,
        right: 30,
    },
    category: {
        minWidth: 200
    }
});

const Home = () => {
    const classes = useStyles();

    const [products, setProducts] = useState(null);
    const [cart, setCart] = useState({
        subTotal: 0,
        igv: 0,
        total: 0
    });
    const [user, setUser] = useState({
        name: "",
        token: null,
        shopCart: null
    });
    const [openUser, setOpenUser] = React.useState(false);
    const [open, setOpen] = React.useState(false);
    const [items, setItems] = useState([]);
    const [error, setError] = useState(null);
    const [category, setCategory] = useState(0);
    const [categories, setCategories] = useState([]);
    const [stock, setStock] = useState(false);
    const [stockList, setStockList] = useState([]);

    const [, updateState] = useState();
    const forceUpdate = useCallback(() => updateState({}), []);

    const handleProduct = (product) => {
        var list = items;
        var uCart = cart;
        if (user.token == null) {
            setOpenUser(true);
        } else {
            var index = list.findIndex(x => x.productId == product.id);
            var productIndex = products.findIndex(x => x.id == product.id);

            if (product.quantity <= 0) {
                setStock(true);
                setStockList([...stockList, product.name])
                return;
            }

            var amount = 0;
            if (index < 0) {
                var item = {
                    productId: product.id,
                    amount: product.price,
                    quantity: 1,
                    product
                };
                list = [...list, item];
                var index = list.findIndex(x => x.productId == product.id);
                amount = item.amount;
            } else {
                list[index].quantity += 1;
                amount = list[index].amount;
            }
            products[productIndex].quantity -= 1;

            cart.subTotal += _.round(amount, 2);
            cart.igv = _.round(cart.subTotal / 0.18, 2);
            cart.total = _.round(cart.subTotal + cart.igv, 2);

            setProducts(products);
            setCart(uCart);
            setItems(list);

            setOpen(true);
        }
    }

    const handleAdd = (item, add = 1) => {
        var list = items;
        var uCart = cart;
        var uStockList = stockList;
        var productIndex = products.findIndex(x => x.id == item.productId);

        if (products[productIndex].quantity == 0 && add > 0) {
            setStock(true);
            if (uStockList.findIndex(x => x == products[productIndex].name) < 0) {
                uStockList.push(products[productIndex].name);
                setStockList(uStockList);
            }
            return;
        }else {
            uStockList.pop(products[productIndex].name);
            setStock(stockList.length > 0);
            setStockList(uStockList);
        }

        var index = list.findIndex(x => x.productId == item.productId);
        if (index < 0) return;
        list[index].quantity += add;
        products[productIndex].quantity += (add * -1);
        if (list[index].quantity == 0) {
            list.splice(index, 1);
        }

        if (add < 0) {
            uCart.subTotal -= _.round(item.amount,2);
            uCart.igv = _.round(uCart.subTotal * 0.18, 2);
            uCart.total = _.round(uCart.subTotal + uCart.igv, 2);
        } else {
            uCart.subTotal += item.amount;
            uCart.igv = _.round(uCart.subTotal * 0.18, 2);
            uCart.total = _.round(uCart.subTotal + uCart.igv,2);
        }

        setProducts(products);
        setCart(uCart);
        setItems(list);

        forceUpdate();
    };

    const handleSaveUser = () => {
        if (user.token == null) {
            Axios.post('/api/user', user).then((resp) => {
                setUser(resp.data);
                localStorage.setItem('user', JSON.stringify(resp.data));
                setOpenUser(false);
            });
        }
    }

    const handleSave = () => {
        var list = [];
        if (items.length <= 0) return;

        items.map((item) => {
            list.push({
                productId: item.productId,
                amount: item.amount,
                quantity: item.quantity,
            });
        })

        Axios.post("/api/shopping",
            { ...cart, items: list})
        .then((resp) => {
            setCart(resp.data);
            setItems(resp.data.items);
            Axios.get(category == 0 ? '/api/products' : '/api/categories/' + category)
                .then(function (resp) {
                    setProducts(category == 0 ? resp.data : resp.data.products);
                });

            forceUpdate();
            setOpen(false);
        }).catch((resp) => {
            setError(resp.data);
            forceUpdate();
            setOpen(false);
        });
    }

    const handleChangeUser = (val) => setUser({ ...user, name: val.currentTarget.value })

    useEffect(() => {
        Axios.get(category == 0 ? '/api/products' : '/api/categories/' + category)
            .then(function (resp) {
                setProducts(category == 0 ? resp.data : resp.data.products);
            });

        Axios.get('/api/categories')
            .then(function (resp) {
                setCategories(resp.data);
            });

        var user = localStorage.getItem('user');        
        if (user != null) {
            setUser(JSON.parse(user));
        }
    }, []);

    useEffect(() => {
        Axios.get(category == 0 ? '/api/products' : '/api/categories/' + category)
            .then(function (resp) {
                setProducts(category == 0 ? resp.data : resp.data.products);
            });
    }, [category]);

    return (
        <Container>
            {error != null && (
                <div>
                    <br />
                    <Alert severity="error">
                        {error}
                    </Alert>
                    <br />
                </div>
            )}
            <h1>
                Productos&nbsp;&nbsp;&nbsp;
                <TextField
                    select
                    label="Categorias"
                    value={category}
                    onChange={(e) => setCategory(e.target.value)}
                    size="small"
                    className={classes.category}
                >
                    <MenuItem value={0}>
                        Todos los productos
                    </MenuItem>
                    {categories.map((option) => (
                        <MenuItem key={option.id} value={option.id}>
                            {option.name}
                        </MenuItem>
                    ))}
                </TextField>
            </h1>
            <Grid spacing={2} container>
                {products == null && (
                    <Grid item xs={12}>
                        <Card>
                            <h4 align="center">Sin Productos</h4>
                        </Card>
                    </Grid>
                )}
                {products != null && products.map((item) => (
                    <Grid key={"product-" + item.id} item xs={12} sm={6} md={4} lg={3}>
                        <Card className={classes.root}>
                            <CardActionArea onClick={() => handleProduct(item)}>
                                {item.image == null && (
                                    <CardMedia
                                        className={classes.media}
                                        image="/images/undefined.png"
                                        title={item.name}
                                    />
                                )}

                                {item.image != null && (
                                    <CardMedia
                                        className={classes.media}
                                        image={item.image.source}
                                        title={item.name}
                                    />
                                )}

                                <CardContent>
                                    <Typography gutterBottom variant="h5" component="h2">
                                        {item.name}
                                    </Typography>
                                    <Typography variant="body2" color="textSecondary" component="p">
                                        {item.description}
                                    </Typography>
                                    <Typography variant="body2" color="textSecondary" component="p">
                                        <b>Precio:</b> {item.price}
                                    </Typography>
                                    <Typography variant="body2" color="textSecondary" component="p">
                                        <b>Stock:</b> {item.quantity}
                                    </Typography>
                                </CardContent>
                            </CardActionArea>
                            <CardActions>
                                <Button size="small" color="primary" onClick={() => handleProduct(item)}>
                                    A&ntilde;adir al Carrito
                                </Button>
                            </CardActions>
                        </Card>
                    </Grid>
                ))}
            </Grid>

            <Dialog
                open={openUser}
                onClose={() => setOpenUser(false)}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">{"¿Cual es tu nombre?"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        <TextField id="name" label="Ingresa tu nombre." value={user.name} onChange={handleChangeUser} />
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenUser(false)} color="primary">
                        Cancelar
                    </Button>
                    <Button onClick={handleSaveUser} color="primary" autoFocus>
                        Guardar
                    </Button>
                </DialogActions>
            </Dialog>

            <Dialog
                open={open}
                onClose={() => setOpen(false)}
                maxWidth='sm'
                fullWidth
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">{"Carrito de Compra"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        <Card>
                            <CardContent>
                                {items != null && items.length > 0 ? items.map((item, i) => (
                                    <Grid key={"item-" + i} container>
                                        <Grid xs={6} item>
                                            <Typography gutterBottom variant="h5" component="span">
                                                {item.product.name}
                                            </Typography>
                                        </Grid>
                                        <Grid xs item>
                                            <Typography variant="body2" color="textSecondary" component="span">
                                                {item.quantity}
                                            </Typography>
                                        </Grid>
                                        <Grid xsitem>
                                            <Typography variant="body2" color="textSecondary" component="span">
                                                {item.amount}
                                            </Typography>
                                        </Grid>
                                        <Grid xs item>
                                            <IconButton size={'small'} color="primary" component="span" onClick={() => handleAdd(item)}>
                                                <AddIcon />
                                            </IconButton>
                                            <IconButton size={'small'} color="primary" component="span" onClick={() => handleAdd(item, -1)}>
                                                <RemoveIcon />
                                            </IconButton>
                                        </Grid>
                                    </Grid>
                                )) : (
                                    <Typography variant="body2" color="textSecondary" component="span">
                                        Sin Compras
                                    </Typography>
                                )}
                            </CardContent>
                        </Card>
                        {stock && (
                            <div>
                                <br />
                                <Alert severity="error">
                                    <AlertTitle>Los siguientes los productos esta sin stock</AlertTitle>.
                                    <ul>
                                        {stockList.map(x => <li>{x}</li>)}
                                    </ul>
                                </Alert>
                            </div>
                        )}
                        <br />
                        <Grid container>
                            <Grid xs={12} item>
                                <Typography gutterBottom variant="body" component="span">
                                    <b>Subtotal: </b>{cart.subTotal}
                                </Typography>
                            </Grid>
                            <Grid xs={12} item>
                                <Typography gutterBottom variant="body" component="span">
                                    <b>IGV: </b>{cart.igv}
                                </Typography>
                            </Grid>
                            <Grid xs={12} item>
                                <Typography gutterBottom variant="body" component="span">
                                    <b>Total: </b>{cart.total}
                                </Typography>
                            </Grid>
                        </Grid>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleSave} color="secondary" disabled={items.length <= 0}>
                        Guardar
                    </Button>
                    <Button onClick={() => setOpen(false)} color="primary" autoFocus>
                        A&ntilde;adir Mas...
                    </Button>
                </DialogActions>
            </Dialog>

            <Fab className={classes.fab} color="secondary" aria-label="edit" onClick={() => setOpen(true)}>
                <ShoppingCartIcon />
            </Fab>
      </Container>
    );
}

export default Home;