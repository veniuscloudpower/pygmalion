import Vue from 'vue'
import Router from "vue-router";

import Body from '../components/body'

import HomePage from '../pages/homepage'
import Login from '../components/login'
import Logout from '../components/logout'
// component

import KubernetesHome from '../pages/kubernetes/index'
import KubernetesProfiles from '../pages/kubernetes/profiles'
import  KubernetesCreateProfile from '../pages/kubernetes/createProfile'

import  TasksHome from '../pages/Tasks/index'
import  TasksCategories from '../pages/Tasks/Categories'
import  TasksCreateTask from '../pages/Tasks/createTask'

import  KBHome from '../pages/KBase/index'
import  KBCreateCategory from '../pages/KBase/createCategory'
import  KBRemoveCategory from '../pages/KBase/removeCategory'
import  KBCreateArticle from '../pages/KBase/createArticle'


import  ProjectsCreate from '../pages/projects/create'
import  ProjectsSwitch from '../pages/projects/Switch'
import  ProjectsRemove from '../pages/projects/Remove'

import  OrganizationUsers from '../pages/organization/Users'

Vue.use(Router)

const routes = [
  { path: '', redirect: { name: 'default' }},
  {
    path: "/login",
    component: Login,
  },
  {
    path: "/logout",
    component: Logout,
  },
  {
    path: '/dashboard',
    component: Body,
    children: [
    {
      path: '',
      name: 'default',
      component:HomePage ,
      meta: {
        title: 'Kubernetes Dashboard | Manage your clusters online.',
      }
    }
    ]
  }
  ,
  {
    path: '/kubernetes',
    component: Body,
    children: [
      {
        path: 'dashboard',
        name: 'KubernetesHome',
        component: KubernetesHome,
        meta: {
          title: 'Kubernetes Dashboard | Kubernetes Service',
        }
      },
      {
        path: 'profiles',
        name: 'KubernetesProfiles',
        component: KubernetesProfiles,
        meta: {
          title: 'Kubernetes Dashboard | Kubernetes Profiles',
        }
      },
      {
        path: 'createProfile',
        name: 'KubernetesCreateProfile',
        component: KubernetesCreateProfile,
        meta: {
          title: 'Kubernetes Dashboard | Kubernetes Create Profile',
        }
      },
      ]
  },
  {
    path: '/Tasks',
    component: Body,
    children: [
      {
        path: 'List',
        name: 'TasksHome',
        component:TasksHome ,
        meta: {
          title: 'Kubernetes Dashboard | Manage your tasks online.',
        }
      },
      {
        path: 'Categories',
        name: 'TasksCategories',
        component:TasksCategories ,
        meta: {
          title: 'Kubernetes Dashboard | Manage your categories for your tasks.',
        }
      },
      {
        path: 'createTask',
        name: 'TasksCreateTask',
        component:TasksCreateTask ,
        meta: {
          title: 'Kubernetes Dashboard | Create tasks.',
        }
      }
    ]
  }
  ,
  {
    path: '/KBase',
    component: Body,
    children: [
      {
        path: 'Categories',
        name: 'KBHome',
        component:KBHome ,
        meta: {
          title: 'Kubernetes Dashboard | Create your knowledge base online.',
        }
      }
      ,
      {
        path: 'createCategory',
        name: 'KBCreateCategory',
        component:KBCreateCategory ,
        meta: {
          title: 'Kubernetes Dashboard | Create a new category for your knowledge base.',
        }
      }
      ,
      {
        path: 'removeCategory',
        name: 'KBRemoveCategory',
        component:KBRemoveCategory,
        meta: {
          title: 'Kubernetes Dashboard | Remove category from your knowledge base.',
        }
      }
      ,
      {
        path: 'createArticle',
        name: 'KBCreateArticle',
        component:KBCreateArticle ,
        meta: {
          title: 'Kubernetes Dashboard | Create article.',
        }
      }
    ]
  },
  {
    path: '/Projects',
    component: Body,
    children: [
      {
        path: 'create',
        name: 'ProjectsCreate',
        component:ProjectsCreate ,
        meta: {
          title: 'Kubernetes Dashboard | Manage your projects online.',
        }
      },
      {
        path: 'Switch',
        name: 'ProjectsSwitch',
        component:ProjectsSwitch ,
        meta: {
          title: 'Kubernetes Dashboard | Switch to another project.',
        }
      },
      {
        path: 'Remove',
        name: 'ProjectsRemove',
        component:ProjectsRemove ,
        meta: {
          title: 'Kubernetes Dashboard | Remove project, action cannot be revoked.',
        }
      }
    ]
  },
  {
    path: '/Organization',
    component: Body,
    children: [
      {
        path: 'Users',
        name: 'OrganizationUsers',
        component:OrganizationUsers ,
        meta: {
          title: 'Kubernetes Dashboard | Create your knowledge base online.',
        }
      }
    ]
  }
];

const router = new Router({
  routes,
  base: '/',
  mode: 'history',
  linkActiveClass: "active",
  scrollBehavior () {
    return { x: 0, y: 0 }
  }
});

router.beforeEach((to, from, next) => {
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  // trying to access a restricted page + not logged in
  // redirect to login page
  if (authRequired && !loggedIn) {
    next('/login');
  } else {
    next();
  }
});

export default router
