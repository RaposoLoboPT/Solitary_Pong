using Raylib_cs;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Solitary Pong");

        Image icon = Raylib.LoadImage("./Icon/Solitary_Pong_Icon.png");

        Raylib.SetWindowIcon(icon);

        Raylib.SetTargetFPS(60);

        // Variaveis que usei.

        // Raquete.

        // Altura do raquete.
        int racketHei = 75;
        // Largura da raquete.
        int racketWid = 10;
        // Possição inicial Y do raquete.
        int racketY = 300 - racketHei / 2;
        // Possição inicial X da raquete.
        int racketX = 50;
        // Velocidade para baixo da raquete.
        int racketSpeedDown = 1;
        // Velocidade para cima da raquete.
        int racketSpeedUp = 1;

        // Bola.

        // Posição inicial X da bola.
        int ballX = 75;
        // Posição inicial Y da bola.
        int ballY = 300;
        // Raio da bola.
        int ballRadius = 10;
        // Velocidade X da bola.
        int ballSpeedX = 5;
        // Velocidade Y da bola.
        int ballSpeedY = 5;

        int score = 0;

        string version = "1.0";

        var color = Color.White;

        // Textos.

        // Texto game over.

        // Largura do texto game over.
        int textGameOverWid = Raylib.MeasureText("Game Over", 50);
        // Posição do texto game over.
        int textGameOverX = 400 - (textGameOverWid / 2);

        // Texto score.

        // Largura do texto score.
        int textScoreWid = Raylib.MeasureText($"Score: {score}", 50);
        // Posição do texto score.
        int textScoreX = 400 - (textScoreWid / 2);

        // Texto reset.

        // Largura do texto reset.
        int textResetWid = Raylib.MeasureText("Press R to reset", 25);
        // Posição do texto reset.
        int textResetX = 400 - (textResetWid / 2);

        Sound gameOverSound;
        Sound scoreUpSound;
        Sound wallImpactSound;

        bool soundPlayed = false;

        Raylib.InitAudioDevice();
        gameOverSound = Raylib.LoadSound("./Sound/Game_Over.wav");
        scoreUpSound = Raylib.LoadSound("./Sound/Score_Up.wav");
        wallImpactSound = Raylib.LoadSound("./Sound/Wall_Impact.wav");

        while (!Raylib.WindowShouldClose())
        {

            // Movimento do jogador.

            if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up))
            {
                racketY -= racketSpeedUp;
            }
            if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down))
            {
                racketY += racketSpeedDown;
            }
            if (racketY <= 0)
            {
                racketSpeedUp = 0;
            }
            else
            {
                racketSpeedUp = 10;
            }
            if (racketY + racketHei >= 600)
            {
                racketSpeedDown = 0;
            }
            else
            {
                racketSpeedDown = 10;
            }

            // Movimento da bola.

            if (true)
            {
                ballX += ballSpeedX;
                ballY -= ballSpeedY;
            }
            if (ballX - ballRadius <= 0)
            {
                ballSpeedX = 0;
                ballSpeedY = 0;
                racketSpeedDown = 0;
                racketSpeedUp = 0;

                if (soundPlayed == false)
                {
                    Raylib.PlaySound(gameOverSound);
                    soundPlayed = true;
                }

                Raylib.BeginDrawing();
                Raylib.DrawText("Game Over", textGameOverX, 200, 50, Color.White);
                Raylib.DrawText($"Score: {score}", textScoreX, 250, 50, Color.White);
                Raylib.DrawText("Press R to reset", textResetX, 300, 25, Color.White);
                color = Color.Gray;
                if (Raylib.IsKeyPressed(KeyboardKey.R))
                {
                    score = 0;
                    ballX = 75;
                    ballY = 300;
                    ballSpeedX = 5;
                    ballSpeedY = 5;
                    racketY = 300 - racketHei / 2;
                    racketX = 50;
                    racketSpeedDown = 1;
                    racketSpeedUp = 1;
                    color = Color.White;
                    soundPlayed = false;
                }
            }
            if (ballX + ballRadius >= 775)
            {
                ballSpeedX *= -1;
                score++;
                Raylib.PlaySound(scoreUpSound);
            }
            if (ballY - ballRadius <= 0)
            {
                ballSpeedY *= -1;
                Raylib.PlaySound(wallImpactSound);
            }
            if (ballY + ballRadius >= 600)
            {
                ballSpeedY *= -1;
                Raylib.PlaySound(wallImpactSound);
            }

            // Ação do jogador na bola.

            if (ballX <= racketX + racketWid && ballY >= racketY && ballY <= racketY + racketHei)
            {
                ballSpeedX *= -1;
                Raylib.PlaySound(wallImpactSound);
            }

            // Desenho do jogo.
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText($"Score: {score}", 625, 20, 30, color);
            Raylib.DrawText($"V.{version}", 700, 570, 30, color);
            Raylib.DrawCircle(ballX, ballY, ballRadius, color);
            Raylib.DrawRectangle(racketX, racketY, racketWid, racketHei, color);
            Raylib.DrawRectangle(775, 0, 25, 600, color);
            Raylib.EndDrawing();

        }
        // Fechar a janela e os sons.
        Raylib.UnloadSound(gameOverSound);
        Raylib.UnloadSound(scoreUpSound);
        Raylib.UnloadSound(wallImpactSound);
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}
