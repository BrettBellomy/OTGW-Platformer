namespace Platform_Test
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight, jumping, isGameOver, hasAxe;
        bool beastIsAlive = true;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 15;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemySpeed = 1;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            Candy.Text = "CANDY: " + score + "/20";

            WIRT.Top += jumpSpeed;

            if (goLeft == true)
            {
                WIRT.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                WIRT.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -10;
                force -= 1;
            }
            else jumpSpeed = 10;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (WIRT.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            WIRT.Top = x.Top - WIRT.Height;
                        }
                    }
                    if ((string)x.Tag == "candy")
                    {
                        if (WIRT.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy") 
                    {
                        if (WIRT.Bounds.IntersectsWith(x.Bounds) && hasAxe == false)
                        {
                            timer1.Stop();
                            isGameOver = true;
                            Candy.Text = "";
                            GameOver.Visible = true;
                            GameOver.Text = Environment.NewLine + Environment.NewLine + "OH NO, THE BEAST GOT YOU!" + Environment.NewLine + "AINT THAT JUST THE WAY?" + Environment.NewLine + "Press Enter to try again";
                            Candy.Text = "GAME OVER";
                        }
                        if (WIRT.Bounds.IntersectsWith(x.Bounds) && hasAxe == true)
                        {
                            beastIsAlive = false;
                            x.Visible = false;
                        }
                    }
                    if ((string)x.Tag == "axe")
                    {
                        if (WIRT.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = false;
                            hasAxe = true;
                        }
                    }
                    if ((string)x.Tag == "greg")
                    {
                        if (WIRT.Bounds.IntersectsWith(x.Bounds) && beastIsAlive == false && score < 20)
                        {
                            timer1.Stop();
                            isGameOver = true;
                            Candy.Text = "GAME OVER";
                            GameOver.Visible = true; 
                            GameOver.Text = Environment.NewLine + Environment.NewLine + "THANKS FOR SAVING ME, WIRT!" + Environment.NewLine + "BUT WHERE'S THE REST OF MY CANDY?" + Environment.NewLine + "Press Enter to try again";
                        }
                        if (WIRT.Bounds.IntersectsWith(x.Bounds) && beastIsAlive == true)
                        {
                            timer1.Stop();
                            isGameOver = true;
                            Candy.Text = "GAME OVER";
                            GameOver.Visible = true;
                            GameOver.Text = Environment.NewLine + Environment.NewLine + "THANKS FOR SAVING ME, WIRT!" + Environment.NewLine + "BUT IT LOOKS LIKE WE STILL NEED TO SOLVE THE BEAST PROBLEM..." + Environment.NewLine + "Press Enter to try again";
                        }
                        if (WIRT.Bounds.IntersectsWith(x.Bounds) && beastIsAlive == false && score == 20)
                        {
                            timer1.Stop();
                            isGameOver = true;
                            Candy.Text = "GAME OVER";
                            GameOver.Visible = true;
                            GameOver.Text = Environment.NewLine + Environment.NewLine + "YOU DID IT, WIRT!" + Environment.NewLine + "YOU BEAT THE BEAST AND GOT ALL MY CANDY BACK!" + Environment.NewLine + "Press Enter to play again";
                        }
                        if (BEAST.Bounds.IntersectsWith(x.Bounds))
                        {
                            timer1.Stop();
                            isGameOver = true;
                            Candy.Text = "GAME OVER";
                            GameOver.Visible = true;
                            GameOver.Text = Environment.NewLine + Environment.NewLine + "YOU HAVE BEAUTIFUL EYES!" + Environment.NewLine + "OH NO, THE BEAST GOT GREG!" + Environment.NewLine + "Press Enter to try again";
                        }
                    }
                }
            }

            // moving the platforms and enemies

            PLATFORM5.Left -= horizontalSpeed;
            if (PLATFORM5.Left < 0 || PLATFORM5.Left + PLATFORM5.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            PLATFORM4.Top += verticalSpeed;
            if (PLATFORM4.Top < 105 || PLATFORM4.Top > 360)
            {
                verticalSpeed = -verticalSpeed;
            }
            BEAST.Left -= enemySpeed;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            hasAxe = false;
            GameOver.Visible = false;

            Candy.Text = "CANDY: " + score + "/20";

            // resets candy to visible

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reset position of player, platform and enemies

            WIRT.Left = 488;
            WIRT.Top = 587;

            BEAST.Left = 546;
            BEAST.Top = 672;

            PLATFORM4.Left = 12;
            PLATFORM4.Top = 360;

            PLATFORM5.Left = 282;
            PLATFORM5.Top = 279;

            timer1.Start();
        }
    }
}
