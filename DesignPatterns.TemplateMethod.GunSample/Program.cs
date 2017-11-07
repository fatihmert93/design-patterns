using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.TemplateMethod.GunSample
{
    public abstract class Gun
    {
        public void StartShot()
        {
            StartBulletFiring();
            while (HaveBullet)
                Fire();
            Console.WriteLine($"All bullets are fired");
        }

        protected readonly int numberOfBullets;
        protected List<Bullet> Bullets;
        protected List<int> Clip = new List<int>();
        protected int fireTime = 0, clipReloadTime, clipReloadAmount;

        protected Gun(int numberOfBullets)
        {
            this.numberOfBullets = numberOfBullets;
            Bullets = CreateBullets(numberOfBullets).ToList();
        }


        protected abstract void ReloadClip();
        protected abstract void StartBulletFiring();
        protected abstract void Fire();
        protected abstract bool HaveBullet { get; }
        protected abstract List<Bullet> CreateBullets(int count);
    }

    public abstract class Bullet
    {
        protected int size, totalTimeCrash;
        protected bool crashed = false;

        protected int Size
        {
            get => size;
            set => size = value;
        }

        protected Bullet()
        {
            totalTimeCrash = GenerateCrashTime();
        }


        protected abstract int GenerateCrashTime();
        public abstract void Move();


    }

    public class NineMilimeterBullet : Bullet
    {

        public NineMilimeterBullet()
        {

            Size = 9;
        }
        protected override int GenerateCrashTime()
        {
            Random random = new Random();
            int miliseconds = random.Next(200, 700);

            return miliseconds;
        }

        public override void Move()
        {
            Thread.Sleep(totalTimeCrash);
            Console.WriteLine($"The bullet crashed {totalTimeCrash} milisecond's");
            crashed = true;
        }
    }

    public class Pistol : Gun
    {
        public Pistol(int numberOfBullets) : base(numberOfBullets)
        {
            clipReloadTime = 2000;
            clipReloadAmount = 16;
        }

        protected override void ReloadClip()
        {
            Console.WriteLine("Clip reloading !!!");
            List<int> clip = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                clip.Add(i);
            }
            Thread.Sleep(clipReloadTime);
            Clip = clip;
        }

        protected override void StartBulletFiring()
        {
            Console.WriteLine($"Pistol start shooting...");
        }

        protected override void Fire()
        {
            if (Clip.Count == 0)
                ReloadClip();
            var bullet = Bullets.FindLast(v => true);
            bullet.Move();
            Bullets.Remove(bullet);
            var bulletInClip = Clip.FindLast(v => true);
            Clip.Remove(bulletInClip);
            Console.WriteLine($"Bullet fired! {++fireTime}");

        }

        protected override bool HaveBullet => Bullets.Count != 0;

        protected override List<Bullet> CreateBullets(int count)
        {
            List<Bullet> bullets = new List<Bullet>();
            for (int i = 1; i <= count; i++)
            {
                NineMilimeterBullet bullet = new NineMilimeterBullet();
                bullets.Add(bullet);
            }
            return bullets;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Pistol pistol = new Pistol(1000);
            pistol.StartShot();

            Console.ReadKey();
        }
    }
}
