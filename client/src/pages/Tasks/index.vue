<template>
  <div>
    <Breadcrumbs main="" title="Task list" />
  <div class="container-fluid jkanban-container" >
    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-header">
            <h5>Task list<span class="pull-right"><router-link title="Create task" class="btn btn-primary" to="/tasks/createTask"><i class="fa fa-plus-circle"></i></router-link></span></h5>
          </div>
          <div class="card-body">
            <kanban-board :stages="stages" :blocks="blocks">
              <div v-for="stage in stages" :slot="stage" :key="stage">
                <div class="kanban-title-board">{{ stage }}</div>
              </div>
              <div v-for="block in blocks" :slot="block.id" :key="block.id">

                <a class="kanban-box" href="#" >
                  <span class="date">{{ block.date }} </span>
                  <span class="badge f-right" :class="block.badgetype">{{ block.badge }}</span>
                  <h6>{{block.title}}</h6>

                  <div class="media">

                  </div>
                  <div class="d-flex mt-6">
                    <ul class="list">
                      <li><i class="fa fa-comments-o"></i>2</li>
                      <li><i class="fa fa-paperclip"></i>2</li>
                      <li><i class="fa fa-eye"></i></li>
                    </ul>

                  </div>
                </a>
              </div>
            </kanban-board>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
</template>

<script>
export default {
  name: "index",
  data(){
    return{
      stages: ['New', 'In Progress', 'Completed'],
      blocks: [
        {
          id: 1,
          status: 'New',
          title: 'Test Sidebar',
          date: '24/7/21',
          badge: 'urgent',
          badgetype: 'badge-danger',
          discussionpoints: 2,
          attachments:0
        },
        {
          id: 2,
          status: 'In Progress',
          title: 'Design Dashboard',
          date: '20/9/20',
          badge: 'medium',
          badgetype: 'badge-primary',
          discussionpoints: 2,
          attachments:0
        },
        {
          id: 3,
          status: 'In Progress',
          title: 'Test card',
          date: '10/11/20',
          badge: 'low',
          badgetype: 'badge-success',
          discussionpoints: 2,
          attachments:1
        },
        {
          id: 4,
          status: 'New',
          title: 'Dashboard issues',
          date: '03/09/20',
          badge: 'urgent',
          badgetype: 'badge-danger',
          discussionpoints: 2,
          attachments:0
        },
        {
          id: 5,
          status: 'Completed',
          title: 'Design Dashboard',
          date: '24/12/20',
          badge: 'medium',
          badgetype: 'badge-primary',
          discussionpoints: 1,
          attachments:0
        },
        {
          id: 6,
          status: 'New',
          title: 'Test Sidebar',
          date: '31/11/20',
          badge: 'urgent',
          badgetype: 'badge-danger',
          discussionpoints: 3,
          attachments:0

        }
      ],
    }
  },
  methods: {
    updateBlock(id, status) {
      this.blocks.find(b => b.id === Number(id)).status = status;
      //saving TODO..
    },
  },
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  },
  mounted() {
    if (!this.currentUser) {
      this.$router.push('/login');
    }
  }
}
</script>

<style scoped>
.container{
  padding-top: 20px;
}
</style>