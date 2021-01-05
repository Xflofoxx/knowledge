"use strict";
/**
 * Created by Dario on 14/03/2017.
 */

// set the log level to debug
U.setLogLevel(U.LOG_LEVELS.DEBUG);

;(function KNE() {

    var _canvas = U.query("#main-canvas");
    var _ctx = _canvas.getContext("2d");
    var _x = _canvas.width/2;
    var _y = _canvas.height-30;
    var _dx = 2;
    var _dy = -2;
    var _ballRadius = 10;

    var _interval;


    var paddleHeight = 10;
    var paddleWidth = 75;
    var paddleX = (_canvas.width-paddleWidth)/2;

    var rightPressed = false;
    var leftPressed = false;

    var brickRowCount = 3;
    var brickColumnCount = 5;
    var brickWidth = 75;
    var brickHeight = 20;
    var brickPadding = 10;
    var brickOffsetTop = 30;
    var brickOffsetLeft = 30;

    var score = 0;
    var lives = 3;

    var bricks = [];
    for(var c=0; c<brickColumnCount; c++) {
        bricks[c] = [];
        for(var r=0; r<brickRowCount; r++) {
            bricks[c][r] = { x: 0, y: 0, status:1 };
        }
    }

    document.addEventListener("keydown", keyDownHandler, false);
    document.addEventListener("keyup", keyUpHandler, false);
    document.addEventListener("mousemove", mouseMoveHandler, false);

    function keyDownHandler(e) {
        if(e.keyCode == 39) {
            rightPressed = true;
        }
        else if(e.keyCode == 37) {
            leftPressed = true;
        }
    }
    function keyUpHandler(e) {
        if(e.keyCode == 39) {
            rightPressed = false;
        }
        else if(e.keyCode == 37) {
            leftPressed = false;
        }
    }
    function mouseMoveHandler(e) {
        var relativeX = e.clientX - _canvas.offsetLeft;
        if(relativeX > 0 && relativeX < _canvas.width) {
            paddleX = relativeX - paddleWidth/2;
        }
    }

    U.log(U.LOG_LEVELS.INFO, "Initializing game...", _canvas, _ctx);

    function drawPaddle() {
        _ctx.beginPath();
        _ctx.rect(paddleX, _canvas.height-paddleHeight, paddleWidth, paddleHeight);
        _ctx.fillStyle = "#0095DD";
        _ctx.fill();
        _ctx.closePath();
    }

    function drawBall() {
        _ctx.beginPath();
        _ctx.arc(_x, _y, _ballRadius, 0, Math.PI*2);
        _ctx.fillStyle = "#0095DD";
        _ctx.fill();
        _ctx.closePath();
    }

    function drawBricks() {
        for(var c=0; c<brickColumnCount; c++) {
            for(var r=0; r<brickRowCount; r++) {
                if(bricks[c][r].status === 1) {
                    var brickX = (c*(brickWidth+brickPadding))+brickOffsetLeft;
                    var brickY = (r*(brickHeight+brickPadding))+brickOffsetTop;
                    bricks[c][r].x = brickX;
                    bricks[c][r].y = brickY;
                    _ctx.beginPath();
                    _ctx.rect(brickX, brickY, brickWidth, brickHeight);
                    _ctx.fillStyle = "#0095DD";
                    _ctx.fill();
                    _ctx.closePath();
                }
            }
        }
    }

    function drawScore() {
        _ctx.font = "16px Arial";
        _ctx.fillStyle = "#0095DD";
        _ctx.fillText("Score: "+score, 8, 20);
    }

    function drawLives() {
        _ctx.font = "16px Arial";
        _ctx.fillStyle = "#0095DD";
        _ctx.fillText("Lives: "+lives, _canvas.width-65, 20);
    }

    function collisionDetection() {
        for(c=0; c<brickColumnCount; c++) {
            for(r=0; r<brickRowCount; r++) {
                var b = bricks[c][r];
                if(b.status === 1) {
                    if(_x > b.x && _x < b.x+brickWidth && _y > b.y && _y < b.y+brickHeight) {
                        _dy = -_dy;
                        b.status = 0;
                        score++;
                        if(score === brickRowCount*brickColumnCount) {
                            U.log(U.LOG_LEVELS.INFO, "YOU WIN, CONGRATULATIONS!");
                        }
                    }
                }
            }
        }
    }

    function draw(){
        _ctx.clearRect(0, 0, _canvas.width, _canvas.height);
        drawBricks();
        drawBall();
        drawPaddle();
        drawScore();
        drawLives();
        collisionDetection();
        if(_x + _dx > _canvas.width-_ballRadius || _x + _dx < _ballRadius) {
            _dx = -_dx;
        }
        if(_y + _dy < _ballRadius) {
            _dy = -_dy;
        }
        else if(_y + _dy > _canvas.height-_ballRadius) {
            if(_x > paddleX && _x < paddleX + paddleWidth) {
                _dy = -_dy;
            }
            else {
                lives--;
                if(!lives) {
                    U.log(U.LOG_LEVELS.INFO, "GAME OVER");
                }
                else {
                    _x = _canvas.width/2;
                    _y = _canvas.height-30;
                    _dx = 3;
                    _dy = -3;
                    paddleX = (_canvas.width-paddleWidth)/2;
                }

            }
        }

        if(rightPressed && paddleX < _canvas.width-paddleWidth) {
            paddleX += 7;
        }
        else if(leftPressed && paddleX > 0) {
            paddleX -= 7;
        }

        _x += _dx;
        _y += _dy;
        U.log(U.LOG_LEVELS.INFO, "Running...");
        window.requestAnimFrame(draw);
    }

    draw();
})();