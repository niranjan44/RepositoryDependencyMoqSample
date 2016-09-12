[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RepositoryDependency.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RepositoryDependency.App_Start.NinjectWebCommon), "Stop")]

namespace RepositoryDependency.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Repository;
    using Models;
    using Moq;
    using System.Collections.Generic;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //To switch to Database , uncomment below line
            // kernel.Bind<IRepository<EmployeeInfo, int>>().To<EmployeeInfoRepository>();

            //Comment below lines , when application wants to access data from database
            Mock<IRepository<EmployeeInfo, int>> mock = new Mock<IRepository<EmployeeInfo, int>>();

            

            List<EmployeeInfo> employeeList = new List<EmployeeInfo>
            {
                new EmployeeInfo {EmpNo=1,EmpName="John", DeptName="IT", Designation="Engineer", Salary=4000 },
                 new EmployeeInfo {EmpNo=1,EmpName="Ram", DeptName="HR", Designation="HR", Salary=3000 },
                new EmployeeInfo {EmpNo=1,EmpName="Antony", DeptName="IT", Designation="Engineer", Salary=6000 },

            };

            mock.Setup(m => m.Get()).Returns(employeeList);

            kernel.Bind<IRepository<EmployeeInfo, int>>().ToConstant(mock.Object);
        }        
    }
}
